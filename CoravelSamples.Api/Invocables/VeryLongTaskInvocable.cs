using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;

namespace CoravelSamples.Api.Invocables
{
    public class VeryLongTaskInvocable : IInvocable
    {
        private readonly ILogger<VeryLongTaskInvocable> _logger;

        public VeryLongTaskInvocable(ILogger<VeryLongTaskInvocable> logger)
        {
            _logger = logger;
        }

        public async Task Invoke()
        {
            try
            {
                _logger.LogInformation("Starting VeryLongTaskInvocable...");
                await Task.Delay(TimeSpan.FromSeconds(40));
                _logger.LogInformation("VeryLongTaskInvocable was successfully finished.");
            }

            catch (Exception e)
            {
                _logger.LogError(e, "There was an exception in the VeryLongTaskInvocable.");
            }
        }
    }
}