using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Models
{
    public class DataStoreContext : DbContext
    {
        public DataStoreContext (DbContextOptions<DataStoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductsQuantity>()
                .HasKey(t => new { t.ProdId, t.ColorId, t.SizeId });

            modelBuilder.Entity<ProductsQuantity>()//product TO Quantity
                .HasOne(pt => pt.Product)
                .WithMany(p => p.Quantity)
                .HasForeignKey(pt => pt.ProdId);

            modelBuilder.Entity<ProductsQuantity>()//productColor TO Quantity
                .HasOne(pt => pt.ProductColor)
                .WithMany(p => p.Quantity)
                .HasForeignKey(pt => pt.ColorId);

            modelBuilder.Entity<ProductsQuantity>()//productSize TO Quantity
                .HasOne(pt => pt.ProductSize)
                .WithMany(t => t.Quantity)
                .HasForeignKey(pt => pt.SizeId);
        }


        public DbSet<MVC_Project.Models.User> User { get; set; }


        public DbSet<MVC_Project.Models.Product> Product { get; set; }
    }
}
