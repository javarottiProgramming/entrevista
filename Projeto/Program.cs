using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Configuration;

namespace Projeto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                    Serilog.Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .MinimumLevel.Information() // Or a higher level like Warning or Error
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Suppress Microsoft logs
                        .MinimumLevel.Override("Serilog.AspNetCore.RequestLoggingMiddleware", LogEventLevel.Warning) // Suppress specific middleware logs
                        .WriteTo.SQLite(
                            sqliteDbPath: settings.GetConnectionString("BaseLog"),
                            tableName: "Logs")
                        .WriteTo.Console()
                        .CreateLogger();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
