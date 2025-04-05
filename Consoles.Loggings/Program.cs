using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Consoles.Loggings
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<App>();
                })
                .Build();

            var app = host.Services.GetRequiredService<App>();
            app.Run();

            await host.RunAsync();
        }
    }

    public class App
    {
        private readonly ILogger<App> _logger;

        public App(ILogger<App> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogTrace("This is a LogTrace");
            _logger.LogDebug("This is a LogDebug");
            _logger.LogInformation("This is a LogInformation");
            _logger.LogWarning("This is a LogWarning");
            _logger.LogError("This is a LogError");
            _logger.LogCritical("This is a LogCritical");
        }
    }
}
