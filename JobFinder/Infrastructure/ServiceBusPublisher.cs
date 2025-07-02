using Azure.Messaging.ServiceBus;
using JobFinder.Domain;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace JobFinder.Infrastructure
{
    public class ServiceBusPublisher
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public ServiceBusPublisher(IConfiguration configuration)
        {
            _connectionString = configuration["ServiceBus:ConnectionString"]
                ?? throw new InvalidOperationException("ServiceBus:ConnectionString not configured.");

            _queueName = configuration["ServiceBus:QueueName"]
                ?? throw new InvalidOperationException("ServiceBus:QueueName not configured.");
        }

        public async Task SendMessageAsync(JobPosting job)
        {
            await using var client = new ServiceBusClient(_connectionString);
            var sender = client.CreateSender(_queueName);

            var jsonMessage = JsonSerializer.Serialize(job);
            var message = new ServiceBusMessage(jsonMessage);

            await sender.SendMessageAsync(message);
        }
    }
}
