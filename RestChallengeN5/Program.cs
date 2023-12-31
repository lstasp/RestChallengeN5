using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using RestChallengeN5.Extension;
using RestChallengeN5.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbChallengeN5Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

builder.Services.AddElasticsearch(builder.Configuration); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
