using AplicaçãoWebCompleta.Configuration;
using AplicaçãoWebCompleta.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterServices();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IConsultaCep>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br/"))
    .AddTypedClient(c => RestService.For<IConsultaCep>(c));
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
