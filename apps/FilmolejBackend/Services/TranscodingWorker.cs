using FilmolejBackend.Data;
using FilmolejBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FilmolejBackend.Services
{
    public class TranscodingWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TranscodingWorker> _logger;
        private readonly IConfiguration _config;

        private readonly string _basePath;

        public TranscodingWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<TranscodingWorker> logger,
            IConfiguration config)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _config = config;

            _basePath = _config["Storage:BasePath"]!;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Transcoding worker started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<FilmolejDbContext>();

                    var movies = await db.Movies
                        .Where(m => m.Status == MovieStatus.Processing)
                        .Take(2) 
                        .ToListAsync(stoppingToken);

                    if (movies.Count == 0)
                    {
                        await Task.Delay(3000, stoppingToken);
                        continue;
                    }

                    var tasks = movies.Select(m => TranscodeMovie(m, db, stoppingToken));
                    await Task.WhenAll(tasks);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in transcoding worker");
                }

                await Task.Delay(2000, stoppingToken);
            }
        }

        private async Task TranscodeMovie(Movie movie, FilmolejDbContext db, CancellationToken token)
        {
            try
            {
                _logger.LogInformation("Transcoding movie {Id}", movie.Id);

                var inputPath = movie.OriginalFilePath;

                var outputDir = Path.Combine(_basePath, "streams", movie.Id.ToString());
                Directory.CreateDirectory(outputDir);

                var outputPath = Path.Combine(outputDir, "stream.m3u8");

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "ffmpeg",
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false
                    }
                };

                process.StartInfo.ArgumentList.Add("-i");
                process.StartInfo.ArgumentList.Add(inputPath);

                process.StartInfo.ArgumentList.Add("-preset");
                process.StartInfo.ArgumentList.Add("ultrafast");

                process.StartInfo.ArgumentList.Add("-c:v");
                process.StartInfo.ArgumentList.Add("libx264");

                process.StartInfo.ArgumentList.Add("-c:a");
                process.StartInfo.ArgumentList.Add("aac");

                process.StartInfo.ArgumentList.Add("-f");
                process.StartInfo.ArgumentList.Add("hls");

                process.StartInfo.ArgumentList.Add("-hls_time");
                process.StartInfo.ArgumentList.Add("10");

                process.StartInfo.ArgumentList.Add("-hls_list_size");
                process.StartInfo.ArgumentList.Add("0");

                process.StartInfo.ArgumentList.Add(outputPath);

                _logger.LogInformation("FFmpeg CMD: ffmpeg {args}", process.StartInfo.Arguments);

                var stopwatch = Stopwatch.StartNew();

                process.Start();

                var stdOutTask = process.StandardOutput.ReadToEndAsync();
                var stdErrTask = process.StandardError.ReadToEndAsync();

                await process.WaitForExitAsync(token);

                stopwatch.Stop();

                var output = await stdOutTask;
                var error = await stdErrTask;

                _logger.LogInformation("Transcoding time: {Time}s", stopwatch.Elapsed.TotalSeconds);
                _logger.LogInformation("FFmpeg output: {output}", output);
                _logger.LogError("FFmpeg error: {error}", error);

                if (process.ExitCode == 0)
                {
                    movie.Status = MovieStatus.Ready;
                    movie.StreamPath = outputPath;

                    _logger.LogInformation("Movie {Id} transcoded successfully", movie.Id);
                }
                else
                {
                    movie.Status = MovieStatus.Failed;

                    _logger.LogError("FFmpeg failed for movie {Id}", movie.Id);
                }

                await db.SaveChangesAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while transcoding movie {Id}", movie.Id);

                movie.Status = MovieStatus.Failed;
                await db.SaveChangesAsync(token);
            }
        }
    }
}
