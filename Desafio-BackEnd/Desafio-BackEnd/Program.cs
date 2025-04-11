using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Models;
using Desafio_BackEnd.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add FluentValidation
builder.Services.AddScoped<IValidator<Motorcycle>, MotorcycleValidator>();
builder.Services.AddScoped<IValidator<MotorcycleUpdate>, MotorcycleUpdateValidator>();

//Add Services 
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configuração das rotas do MotosController
//app.MapMotosRoutes();

app.Run();
