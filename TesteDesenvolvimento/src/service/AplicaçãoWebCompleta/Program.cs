using AplicaçãoWebCompleta.Configuration;
using AplicaçãoWebCompleta.Services.Interface;
using Microsoft.AspNetCore.Diagnostics;
using Refit;
using System.Net;
using Newtonsoft.Json;
using AplicaçãoWebCompleta.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterServices();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddHttpClient<IConsultaCep>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br/"))
    .AddTypedClient(c => RestService.For<IConsultaCep>(c));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (ex != null)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case BadHttpRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var errorMessage = JsonConvert.SerializeObject(new { error = ex.Message });
            await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
        }
    });
});

app.UseCors("MyCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AplicacaoWebContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar migrações do banco de dados.");
    }
}

app.Run();
