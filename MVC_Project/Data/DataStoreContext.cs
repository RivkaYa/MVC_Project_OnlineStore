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
            //many to many - Quantity table
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



            //Many to many - orders History
            modelBuilder.Entity<OrdersHistoryProductsList>()
               .HasKey( a=> new {a.OrderId, a.ProdId, a.ColorId, a.SizeId });

            modelBuilder.Entity<OrdersHistoryProductsList>()//Orders to OrdersList
                .HasOne(pt => pt.OrdersHistory)
                .WithMany(a => a.OrderItems)
                .HasForeignKey(pt => pt.OrderId);


            modelBuilder.Entity<OrdersHistoryProductsList>()//Product TO OrdersList
                .HasOne(pt => pt.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(pt => pt.ProdId);

            modelBuilder.Entity<OrdersHistoryProductsList>()//Size TO OrdersList
                .HasOne(pt => pt.ProductSize)
                .WithMany(t => t.Orders)
                .HasForeignKey(pt => pt.SizeId);

            modelBuilder.Entity<OrdersHistoryProductsList>()//Color TO OrdersList
              .HasOne(pt => pt.ProductColor)
                .WithMany(p => p.Orders)
                .HasForeignKey(pt => pt.ColorId);



            //Many to many - Product i,ages
            modelBuilder.Entity<ProductsImages>()
               .HasKey(a => new { a.ProdId, a.ColorId});

            modelBuilder.Entity<ProductsImages>()//ProductID
                .HasOne(pt => pt.Product)
                .WithMany(a => a.Images)
                .HasForeignKey(pt => pt.ProdId);


            modelBuilder.Entity<ProductsImages>()//colorID
                .HasOne(pt => pt.ItemColor)
                .WithMany(p => p.Images)
                .HasForeignKey(pt => pt.ColorId);


        }


        public DbSet<MVC_Project.Models.User> User { get; set; }


        public DbSet<MVC_Project.Models.Product> Product { get; set; }
        public DbSet<MVC_Project.Models.ProductCategory> ProductCategory { get; set; }
        public DbSet<MVC_Project.Models.ProductsQuantity> ProductsQuantity { get; set; }
        public DbSet<MVC_Project.Models.ProductSize> ProductSize { get; set; }
        public DbSet<MVC_Project.Models.ProductColor> ProductColor { get; set; }
        public DbSet<MVC_Project.Models.ProductsImages> ProductsImages { get; set; }
        public DbSet<MVC_Project.Models.Cart> Cart { get; set; }
    }
}
