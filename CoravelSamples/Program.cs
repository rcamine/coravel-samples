using Coravel;
using CoravelSamples.Invocables;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoravelSamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Services.UseScheduler(scheduler => 
                scheduler.Schedule<MyInvocableTest>().EveryMinute()
            );

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddTransient<MyInvocableTest>();
                    services.AddScheduler();
                    services.AddQueue();
                });
    }
}
