using PaymentProcessor;
using Restaurant.MessageBus;
using Restaurant.Services.PaymentAPI.Extensions;
using Restaurant.Services.PaymentAPI.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Process Payment
builder.Services.AddSingleton<IProcessPayment, ProcessPayment>();

// ServiceBus Consumer
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

// Message Publisher
builder.Services.AddSingleton<IMessageBus, AzureServiceBusMessageBus>();

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

// Service Bus Extension
app.UseAzureServiceBusConsumer();

app.Run();
