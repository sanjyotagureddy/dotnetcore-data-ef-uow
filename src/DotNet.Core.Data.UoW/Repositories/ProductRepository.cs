using DotNet.Core.Data.UoW.Domain.Entities;
using DotNet.Core.Data.UoW.Persistence;
using DotNet.Core.Data.UoW.Repositories.Interfaces;

namespace DotNet.Core.Data.UoW.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
  {
    public ProductRepository(ApplicationContext dbContext)
      : base(dbContext)
    {
    }

  }
}
