using System;
using System.Threading.Tasks;

namespace DotNet.Core.Data.UoW.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository Products { get; }

        Task<int> SaveChangesAsync();
    }
}