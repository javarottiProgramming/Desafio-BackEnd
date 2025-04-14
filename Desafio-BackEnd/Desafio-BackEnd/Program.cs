using Dapper.FluentMap;
using Desafio_BackEnd.Data;
using Desafio_BackEnd.Data.Repositories;
using Desafio_BackEnd.Domain.Dtos;
using Desafio_BackEnd.Domain.Interfaces.Repositories;
using Desafio_BackEnd.Domain.Interfaces.Services;
using Desafio_BackEnd.Domain.Mappings.Entities;
using Desafio_BackEnd.Domain.Mappings.Profiles;
using Desafio_BackEnd.Domain.Models;
using Desafio_BackEnd.Domain.Validators;
using Desafio_BackEnd.Services;
using FluentValidation;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

//TODO criar arquivos de extensao para diminuir o tamanho do Program.cs

FluentMapper.Initialize(config =>
{
    config.AddMap(new RentalMap());
    config.AddMap(new MotorcycleMap());
});

// Add services to the container.

builder.Services.AddControllers();

// Add FluentValidation
builder.Services.AddScoped<IValidator<MotorcycleDto>, MotorcycleValidator>();
builder.Services.AddScoped<IValidator<MotorcyclePlateUpdateDto>, MotorcycleUpdateValidator>();
builder.Services.AddScoped<IValidator<DeliveryManRequest>, DeliveryManValidator>();
builder.Services.AddScoped<IValidator<CreateRentalModel>, RentalValidator>();

//Add Services
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();
builder.Services.AddScoped<IDeliveryMenService, DeliveryMenService>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddSingleton(new DatabaseConnection(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

// Adicionar Repositories
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();

// Adicionar AutoMapper
builder.Services.AddAutoMapper(typeof(RentalDtoMapperProfile));
builder.Services.AddAutoMapper(typeof(MotorcycleDtoMapperProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//TODO Configurar swagger para aparecer descrição das rotas e esconder schemas
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.MapType<object>(() => new OpenApiSchema { Type = "object", Nullable = true });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });

    app.UseSwaggerUI(o =>
    {
        o.DefaultModelsExpandDepth(-1);
        o.DocumentTitle = "Desafio BackEnd - API";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();