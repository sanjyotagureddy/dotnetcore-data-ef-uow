using DotNet.Core.Data.UoW.Persistence;
using DotNet.Core.Data.UoW.Repositories;
using DotNet.Core.Data.UoW.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet.Core.Data.UoW
{
  public static class ServiceRegistration
  {
    public static IServiceCollection AddDataContextServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("ProductsDb")));

      services.AddTransient<IUnitOfWork, UnitOfWork>();
      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

      services.AddScoped<IProductRepository, ProductRepository>();

      return services;
    }
  }
}
