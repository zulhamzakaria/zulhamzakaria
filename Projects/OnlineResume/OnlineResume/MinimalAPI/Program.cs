using Application.Abstractions.Interfaces;
using Application.Commands.Details;
using Application.Queries.Details;
using Application.Queries.Handlers;
using DataAccess;
using DataAccess.Repositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db DI container
builder.Services.AddDbContext<OnlineResumeDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("ElephantSQL")));

// repos DI container
builder.Services.AddScoped<IDetailRepository, DetailRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IResponsibilityRepository, ResponsibilityRepository>();

// mediatr
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(GetDetailsHandler).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// minimal api
// mediatr bein injected usin method injection since theres no constructor
// WithName() is for assigning name to the endpoint
#region "Details"
app.MapGet("/api/details", async (IMediator mediator) =>
{
    var getDetails = new GetDetails();
    var details = await mediator.Send(getDetails);
    if (details is null)
        return Results.NotFound("No data found.");
    return Results.Ok(details);
}).WithName("GetDetails");

app.MapPost("/api/details", async (IMediator mediator, Detail detail) =>
{
    var createDetails = new CreateDetails
    {
        Email = detail.Email,
        Name = detail.Name,
        PhoneNo = detail.PhoneNo,
        Qualification = detail.Qualification,
        Photo = detail.Photo,
        Role = detail.Role
    };
    var createdDetails = await mediator.Send(createDetails);
    return Results.CreatedAtRoute("GetDetails",null,createdDetails);
}).WithName("CreateDetails");

app.MapPut("/api/posts/{id}", async (IMediator mediator, Detail detail, int id) =>
{
    var updateDetails = new UpdateDetails {
        Id = id,
        Role = detail.Role,
        Email = detail.Email,
        Name = detail.Name, 
        PhoneNo = detail.PhoneNo, 
        Qualification = detail.Qualification, 
        Photo = detail.Photo };
    var updatedDetails = await mediator.Send(updateDetails);
    return Results.Ok(updatedDetails);
}).WithName("UpdateDetails");
#endregion


app.Run();
