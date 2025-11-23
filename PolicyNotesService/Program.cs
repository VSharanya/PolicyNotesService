using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PolicyNotesService.Data;
using PolicyNotesService.Models;
using PolicyNotesService.Repositories;
using PolicyNotesService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NotesDbContext>(opt =>
    opt.UseInMemoryDatabase("NotesDb"));

builder.Services.AddScoped<IPolicyNotesRepository, PolicyNotesRepository>();
builder.Services.AddScoped<PolicyNotesService.Services.PolicyNotesService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/notes", async (PolicyNotesService.Services.PolicyNotesService svc, PolicyNoteCreateDto dto) =>
{
    if (string.IsNullOrWhiteSpace(dto.PolicyNumber))
        return Results.BadRequest("Policy number is required");

    var created = await svc.AddNoteAsync(dto);
    return Results.Created($"/notes/{created.Id}", created);
});

app.MapGet("/notes", async (PolicyNotesService.Services.PolicyNotesService svc) =>
{
    var notes = await svc.GetAllAsync();
    return Results.Ok(notes);
});

app.MapGet("/notes/{id:int}", async (PolicyNotesService.Services.PolicyNotesService svc, int id) =>
{
    var note = await svc.GetByIdAsync(id);
    return note is null ? Results.NotFound() : Results.Ok(note);
});

app.Run();

public partial class Program { }
