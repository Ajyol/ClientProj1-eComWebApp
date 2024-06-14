using eComWebApp.Data.Services;
using eComWebApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// SQL Connection
builder.Services.AddDbContext<ApplicationDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register OrdersService with the dependency injection container
builder.Services.AddScoped<IOrdersService, OrdersService>();

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Fixes the cors error and help fetch from a different endpoint (front-back connection)
app.UseCors(options => options.WithOrigins("https://127.0.0.1:4200")
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
