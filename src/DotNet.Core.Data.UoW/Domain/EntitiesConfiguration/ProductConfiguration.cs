using System.Collections.Generic;
using DotNet.Core.Data.UoW.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNet.Core.Data.UoW.Domain.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("Id");

            entity.HasData(new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Description = "A description",
                    Name = "Product A",
                    Price = 49.99
                },
                new Product()
                {
                    Id = 2,
                    Description = "B description",
                    Name = "Product B",
                    Price = 29.99
                },
                new Product()
                {
                    Id = 3,
                    Description = "C description",
                    Name = "Product C",
                    Price = 89.99
                }
            });
        }
    }
}