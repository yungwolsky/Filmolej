using FilmolejBackend.Data;
using FilmolejBackend.Services;
using FilmolejBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuring DbContext with PostgreSQL connection
builder.Services.AddDbContext<FilmolejDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention());

// Upgrade the limit of sended files
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 2L * 1024 * 1024 * 1024; // 2GB
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2L * 1024 * 1024 * 1024;
});

// Add controllers
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorPages();

// Adding worker
builder.Services.AddHostedService<TranscodingWorker>();

// Adding dependencies
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddHttpClient<IMovieService, MovieService>();

// JWT Token
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "FilmolejBackend",
            ValidAudience = "FilmolejFrontend",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });
builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Environment.EnvironmentName = "Development";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
