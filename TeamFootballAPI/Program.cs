using Microsoft.AspNetCore.SignalR;
using TeamFootballAPI.Hubs;
using TeamFootballAPI.Models;
using TeamFootballAPI.Models.Dto;
using TeamFootballAPI.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<TeamService>();
builder.Services.AddSignalR();
builder.Services.AddControllers();
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

app.MapGet("/api/team", (TeamService teamService) => Results.Ok(teamService.Teams.ToList()));
app.MapGet("/api/team/{id}", (TeamService teamService, Guid id) =>
{
    var team = teamService.Teams.FirstOrDefault(t => t.Id == id);
    return team != null ? Results.Ok(team) : Results.NotFound();
});

app.MapPost("/api/team", async (TeamService teamService, IHubContext<TeamHub> hubContext, TeamCreateDto team) =>
{
    var teamNew = new Team
    {
        Id = Guid.NewGuid(),
        Name = team.Name,
        City = team.City,
        YearFounded = team.YearFounded,
    };
    teamService.Teams.Add(teamNew);
    await hubContext.Clients.All.SendAsync("ReceiveTeam", teamNew);
    //return Results.CreatedAtAction("GetTeamById", new { id = teamNew.Id }, teamNew);
    return Results.Ok();
});

app.Run();
