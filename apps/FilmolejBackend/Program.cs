using FilmolejBackend.Data;
using FilmolejBackend.Services.Interfaces;
using FilmolejBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuring DbContext with PostgreSQL connection
builder.Services.AddDbContext<FilmolejDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages();

// Adding dependencies
builder.Services.AddScoped<IUserRegistryService, UserRegistryService>();
builder.Services.AddScoped<IUploadService, UploadService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
