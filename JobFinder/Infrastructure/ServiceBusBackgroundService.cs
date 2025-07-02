using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure
{
    public class ServiceBusBackgroundService : BackgroundService
    {
        private readonly ServiceBusConsumer _consumer;

        public ServiceBusBackgroundService(ServiceBusConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.ReceiveMessagesAsync();
        }
    }
}
