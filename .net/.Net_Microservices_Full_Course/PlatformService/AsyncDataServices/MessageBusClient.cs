using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PlatformService.DTOs;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration configuration;
        private readonly IConnection connection;
        private readonly IModel channel;

        public MessageBusClient(IConfiguration configuration)
        {
            this.configuration = configuration;
            //RabbitMQ connection factory
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQHost"],
                Port = int.Parse(configuration["RabbitMQPort"])
            };
            try
            {
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: "Trigger", type: ExchangeType.Fanout);
                connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("Connected to Message Bus");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not connect to the message bus : {e.Message}");
            }
        }
        public void PublishNewPlatform(PlatformPublishedDTO platformPublishedDTO)
        {
            var message = JsonSerializer.Serialize(platformPublishedDTO);
            if (connection.IsOpen)
            {
                Console.WriteLine("RabbitMQ connection open");
                //Send message
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("RabbitMQ connection is closed");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Shutting down RabbitMQ connection...");
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"Message sent {message}.");
        }

        public void Dispose(){
            Console.WriteLine("Disposing RabbitMQ");
            if(channel.IsOpen){
                channel.Close();
                connection.Close();
            }
        }
    }
}