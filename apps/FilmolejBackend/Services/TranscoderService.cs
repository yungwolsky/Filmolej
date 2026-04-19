using FilmolejBackend.Data;
using FilmolejBackend.Models;
using FilmolejBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace FilmolejBackend.Services
{
    public class TranscoderService() : ITranscoderService
    {
        public async Task Transcode(string inputPath, string outputDir)
        {
            Directory.CreateDirectory(outputDir);

            var outputPath = Path.Combine(outputDir, "stream.m3u8");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = $"-i \"{inputPath}\" -codec: copy -start_number 0 -hls_time 10 -hls_list_size 0 -f hls \"{outputPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };

            process.Start();
            await process.WaitForExitAsync();
        }
    }
}
