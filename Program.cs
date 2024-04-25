using Microsoft.EntityFrameworkCore;
using ApiNotas.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de MongoDB
var connectionString = builder.Configuration.GetConnectionString("MongoDBConnectionString");
var databaseName = builder.Configuration.GetConnectionString("MongoDBDatabaseName");
builder.Services.AddSingleton(new MongoClient(connectionString));
builder.Services.AddSingleton(new NotasContext(connectionString, databaseName));
builder.Services.AddScoped(provider =>
{
    var mongoClient = provider.GetService<MongoClient>();
    return mongoClient.GetDatabase(databaseName);
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var misReglas = "ReglasCors";
builder.Services.AddCors(op =>
{
    op.AddPolicy(name: misReglas, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(misReglas);
app.UseAuthorization();

app.MapControllers();

app.Run();
