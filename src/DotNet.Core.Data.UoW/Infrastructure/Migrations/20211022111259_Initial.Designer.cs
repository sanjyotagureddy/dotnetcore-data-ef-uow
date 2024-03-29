﻿// <auto-generated />
using DotNet.Core.Data.UoW.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DotNet.Core.Data.UoW.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211022111259_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DotNet.Core.Data.UoW.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A description",
                            Name = "Product A",
                            Price = 49.990000000000002
                        },
                        new
                        {
                            Id = 2,
                            Description = "B description",
                            Name = "Product B",
                            Price = 29.989999999999998
                        },
                        new
                        {
                            Id = 3,
                            Description = "C description",
                            Name = "Product C",
                            Price = 89.989999999999995
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
