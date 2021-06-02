using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Core.Data.UoW.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DotNet.Core.Data.UoW.Persistence
{
    public class ApplicationContextSeed
    {
        public static async Task SeedAsync(ApplicationContext context, ILogger<ApplicationContextSeed> logger)
        {
            if (!context.Products.Any())
            {
                await context.Products.AddRangeAsync(GetPreconfiguredOrders());
                await context.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", nameof(ApplicationContext));
            }
        }

        private static IEnumerable<Product> GetPreconfiguredOrders()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Description = "A description",
                    Name = "Product A",
                    Price = 49.99
                },
                new Product()
                {
                    Description = "B description",
                    Name = "Product B",
                    Price = 29.99
                },
                new Product()
                {
                    Description = "C description",
                    Name = "Product C",
                    Price = 89.99
                }
            };
        }
    }
}