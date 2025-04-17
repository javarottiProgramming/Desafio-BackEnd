using Dapper.FluentMap;
using Challenge.BackEnd.Infrastructure.Data;
using Challenge.BackEnd.Infrastructure.Data.Repositories;
using Challenge.BackEnd.Core.Domain.Dtos;
using Challenge.BackEnd.Core.Domain.Interfaces.Repositories;
using Challenge.BackEnd.Core.Domain.Interfaces.Services;
using Challenge.BackEnd.Core.Domain.Mappings.Entities;
using Challenge.BackEnd.Core.Domain.Mappings.Profiles;
using Challenge.BackEnd.Core.Domain.Models;
using Challenge.BackEnd.Core.Domain.Validators;
using Challenge.BackEnd.Core.Services;
using FluentValidation;
using MassTransit;
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
    config.AddMap(new DeliveryManMap());

});

// Add services to the container.

builder.Services.AddControllers();

// Add FluentValidation
builder.Services.AddScoped<IValidator<MotorcycleDto>, MotorcycleValidator>();
builder.Services.AddScoped<IValidator<MotorcyclePlateUpdateDto>, MotorcycleUpdateValidator>();
builder.Services.AddScoped<IValidator<DeliveryManDto>, DeliveryManValidator>();
builder.Services.AddScoped<IValidator<CreateRentalModel>, RentalValidator>();

//Add Services
builder.Services.AddScoped<IMotorcycleService, MotorcycleService>();
builder.Services.AddScoped<IDeliveryManService, DeliveryManService>();
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddSingleton(new DatabaseConnection(builder.Configuration.GetConnectionString("PostgreSqlConnection")));

// Adicionar Repositories
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();

// Adicionar AutoMapper
builder.Services.AddAutoMapper(typeof(RentalMapperProfile));
builder.Services.AddAutoMapper(typeof(MotorcycleMapperProfile));
builder.Services.AddAutoMapper(typeof(DeliveryManMapperProfile));

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

builder.Services.AddMassTransit(bus =>
{
    bus.UsingRabbitMq((ctx, busConfigurator) =>
    {
        busConfigurator.Host(builder.Configuration.GetConnectionString("RabbitMq"));
    });
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