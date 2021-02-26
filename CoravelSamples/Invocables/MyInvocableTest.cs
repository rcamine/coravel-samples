using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using System;

namespace CoravelSamples.Invocables
{
    public class MyInvocableTest : IInvocable
    {
        private readonly ILogger<MyInvocableTest> _logger;

        public MyInvocableTest(ILogger<MyInvocableTest> logger)
        {
            _logger = logger;
        }

        public Task Invoke()
        {
            _logger.LogInformation($"MyInvocableTest was invoked at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}