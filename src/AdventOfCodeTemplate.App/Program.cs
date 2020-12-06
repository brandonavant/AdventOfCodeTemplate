using System.IO;
using System.Threading.Tasks;
using AdventOfCodeTemplate.App.Services;
using AdventOfCodeTemplate.Base.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AdventOfCodeTemplate.App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build configuration
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            // Configure SeriLog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ISolutionService, SolutionService>();
                    services.AddScoped<IInputService, InputService>();
                })
                .UseSerilog()
                .Build();

            var solutionService = ActivatorUtilities.CreateInstance<SolutionService>(host.Services);

            solutionService.Run().GetAwaiter().GetResult();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
