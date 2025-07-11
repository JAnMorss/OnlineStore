using OnlineStore.API.Extensions;
using OnlineStore.Application;
using OnlineStore.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
