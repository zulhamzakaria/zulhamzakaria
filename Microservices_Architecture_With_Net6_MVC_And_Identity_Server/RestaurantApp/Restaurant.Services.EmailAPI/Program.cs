using Microsoft.EntityFrameworkCore;
using Restaurant.Services.EmailAPI.Extensions;
using Restaurant.Services.EmailAPI.Infrastructure;
using Restaurant.Services.EmailAPI.Messaging;
using Restaurant.Services.EmailAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db Context
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlConnection"]));
var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionBuilder.UseSqlServer(builder.Configuration["ConnectionStrings:SqlConnection"]);
builder.Services.AddSingleton(new EmailRepository(optionBuilder.Options));

// Service Bus Extension
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

// Repository
builder.Services.AddScoped<IEmailRepository, EmailRepository>();


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

// Messaging
app.UseAzureServiceBusConsumer();

app.Run();
