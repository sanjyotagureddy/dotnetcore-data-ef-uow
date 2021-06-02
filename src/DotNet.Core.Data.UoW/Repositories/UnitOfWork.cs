using System.Threading.Tasks;
using DotNet.Core.Data.UoW.Persistence;
using DotNet.Core.Data.UoW.Repositories.Interfaces;

namespace DotNet.Core.Data.UoW.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public IProductRepository Products { get; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}