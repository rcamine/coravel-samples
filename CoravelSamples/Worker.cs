using System;
using System.Threading;
using System.Threading.Tasks;
using Coravel.Queuing.Interfaces;
using CoravelSamples.Invocables;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoravelSamples
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueue _queue;

        public Worker(ILogger<Worker> logger, IQueue queue)
        {
            _logger = logger;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queueing MyInvocableTest in the memory queue.");
            _queue.QueueInvocable<MyInvocableTest>();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
