using DotNet.Core.Data.UoW.Domain.Entities;
using DotNet.Core.Data.UoW.Domain.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DotNet.Core.Data.UoW.Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public void SetDatabaseTimeOut(int seconds)
        {
            this.Database.SetCommandTimeout(seconds);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Entities Configurations

            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());

            #endregion

        }

    }
}