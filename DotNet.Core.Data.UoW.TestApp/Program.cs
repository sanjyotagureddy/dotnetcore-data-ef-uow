using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DotNet.Core.Data.UoW.Infrastructure.Extensions;
using DotNet.Core.Data.UoW.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet.Core.Data.UoW.TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.MigrateDatabase<ApplicationContext>((context, service) =>
            {
                var logger = service.GetService<ILogger<ApplicationContextSeed>>();
                ApplicationContextSeed
                    .SeedAsync(context, logger)
                    .Wait();
            });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
