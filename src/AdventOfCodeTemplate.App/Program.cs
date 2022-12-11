using System;
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
            var configuration = BuildConfig(builder);

            // Configure SeriLog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<ISolutionService, SolutionService>();
                    services.AddScoped<IInputService, InputService>();
                    services.AddSingleton<IConfiguration>(configuration);
                })
                .UseSerilog()
                .Build();

            var solutionService = ActivatorUtilities.CreateInstance<SolutionService>(host.Services);

            try
            {
                solutionService.Run().GetAwaiter().GetResult();
            }
            catch (ArgumentException ex)
            {
                Log.Logger.Error(ex, "Invalid input/configuration.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "An unhandled exception has occurred.");
            }
        }

        static IConfiguration BuildConfig(IConfigurationBuilder builder)
        {
            return builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
