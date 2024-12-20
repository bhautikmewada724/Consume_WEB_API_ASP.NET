using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using WebAPI_Practice;
using WebAPI_Practice.Data;
using WebAPI_Practice.Model;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CityRepository for dependency injection
builder.Services.AddScoped<CityRepository>();
builder.Services.AddScoped<CountryRepository>();
builder.Services.AddScoped<StateRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
