using Car_Stock_API.Data;
using Car_Stock_API.Repositories;
using Car_Stock_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// Add services to the container.
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .Build());
});

services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase(databaseName: "CarStock"); });

services.AddScoped<DataSeeder>();
services.AddScoped<ICarRepository, CarRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/cars", async (ICarRepository carRepository) =>
{
    return await carRepository.Get();
}).WithName("get cars");

app.MapGet("/search", async (string searchTerms, ICarRepository carRepository) => 
{ 
    return await carRepository.GetAsync(searchTerms);
}).WithName("search cars");

app.MapPost("/create", async (Car car, ICarRepository carRepository) => 
{
    await carRepository.Create(car);
}).WithName("create car");

app.MapPost("/edit", async (Car car, ICarRepository carRepository) => 
{
    await carRepository.Update(car);
}).WithName("edit car");

app.MapPost("/remove", async (string carId, ICarRepository carRepository) =>
{
    await carRepository.Delete(carId);
}).WithName("remove car by Id");

app.UseCors();

using(var scope = app.Services.CreateScope())
{
    var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    dataSeeder.Seed();
}

app.Run();
