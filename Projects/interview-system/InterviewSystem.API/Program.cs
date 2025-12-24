using InterviewSystem.API.Extensions;
using InterviewSystem.API.Middlewares;
using InterviewSystem.Application.Candidates.CreateCandidate;
using InterviewSystem.Application.InterviewTasks.GetInterviewTasksSummary;
using InterviewSystem.Domain.Interfaces.Repositories;
using InterviewSystem.Infrastructure;
using InterviewSystem.Infrastructure.CustomQueries.InterviewTasks;
using InterviewSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//Repo DI Containers
builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IInterviewTaskRepository, InterviewTaskRepository>();
builder.Services.AddScoped<IInterviewTaskQueryRepository, InterviewTaskQueryRepository>();
builder.Services.AddScoped<IInterviewRoundRepository, InterviewRoundRepository>();
builder.Services.AddScoped<IInterviewProcessRepository, InterviewProcessRepository>();

//Actions DI Containers
builder.Services.AddScoped<CreateCandidateHandler>();

//mediatr
builder.Services.AddApplicationServices();
//builder.Services.AddMediatR(cfg =>
//    cfg.RegisterServicesFromAssembly(typeof(CreateCandidateCommand).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
