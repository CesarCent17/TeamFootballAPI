using Microsoft.AspNetCore.SignalR;
using TeamFootballAPI.Hubs;
using TeamFootballAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<TeamService>();
builder.Services.AddSignalR();
//builder.Services.AddTransient<IHubContext<TeamHub>>();
//builder.Services.AddTransient<IHubContext<TeamHub>, HubContext<TeamHub>>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
              .WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllHeaders");

app.UseAuthorization();

app.MapHub<TeamHub>("/teamHub");

app.MapControllers();

app.Run();
