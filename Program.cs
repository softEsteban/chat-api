using System.Net.Mime;
using System;
using System.Collections.Immutable;
using ChatApi.Services;
using ChatApi.Models;
using ChatApi.DTOS;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "Real Time Chat API",
            Description =
                "An ASP.NET Core Web API for managing user authentication and Real time chat"
        }
    );
});

// Inject service with singleton
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<ChatService>();

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.Configure<ChatApiDatabaseSettings>(
    builder.Configuration.GetSection("ChatApiDatabase")
);

builder.Services.AddSignalR();
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200"));

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.MapHub<ChatHub>("hubs/chat");

app.Run();
