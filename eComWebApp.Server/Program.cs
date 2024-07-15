using eComWebApp.Data;
using eComWebApp.Data.Services;
using eComWebApp.Server.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    // Password requirements configuration
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Register OrdersService with the dependency injection container
builder.Services.AddScoped<IOrdersService, OrdersService>();


// Register EmailService
builder.Services.AddSingleton<IEmailService>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var smtpSettings = config.GetSection("SmtpSettings");
    return new EmailService(
        smtpSettings["Server"],
        int.Parse(smtpSettings["Port"]),
        smtpSettings["User"], // Corrected to Username
        smtpSettings["Pass"],
        sp.GetRequiredService<ILogger<EmailService>>() // Inject ILogger<EmailService>

    );
});

// Add Data Protection
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"c:\keys")) // Ensure this path exists and is writable
    .SetApplicationName("eComWebApp");

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        // Delete and recreate the database
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        await AppDbInitializer.Initialize(dbContext, logger);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error initializing the database: {ex.Message}");
        throw;
    }
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors(options => options.WithOrigins("https://127.0.0.1:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
