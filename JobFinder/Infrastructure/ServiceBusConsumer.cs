using Azure.Messaging.ServiceBus;
using JobFinder.Domain;
using JobFinder.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure
{
    public class ServiceBusConsumer
    {
        private readonly string connectionString =
            "<YOUR_…>";

        private readonly string queueName = "jobcreatedqueue";
        private readonly IServiceScopeFactory _scopeFactory;

        public ServiceBusConsumer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task ReceiveMessagesAsync()
        {
            await using var client = new ServiceBusClient(connectionString);
            var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();

            Console.WriteLine("Consumer listening... Press any key to stop.");
            Console.ReadKey();

            await processor.StopProcessingAsync();
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var body = args.Message.Body.ToString();

            var jobPosting = JsonSerializer.Deserialize<JobPosting>(body);

            if (jobPosting is null)
            {
                Console.WriteLine("Gelen mesaj deserialize edilemedi.");
                await args.AbandonMessageAsync(args.Message);
                return;
            }

            Console.WriteLine($"Mesaj alındı: {jobPosting.Title} | {jobPosting.Description}");

            // SCOPED SERVICE BURADA ALINIYOR
            using (var scope = _scopeFactory.CreateScope())
            {
                var jobAlertService = scope.ServiceProvider.GetRequiredService<JobAlertService>();

                await jobAlertService.RunJobAlertsAsync(jobPosting);
            }

            await args.CompleteMessageAsync(args.Message);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Hata oluştu: {args.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}
