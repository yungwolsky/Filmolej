using FilmolejBackend.Data;
using FilmolejBackend.Services.Interfaces;
using FilmolejBackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuring DbContext with PostgreSQL connection
builder.Services.AddDbContext<FilmolejDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention());

// Add controllers
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorPages();

// Adding dependencies
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<ITokenService, TokenService>();

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
