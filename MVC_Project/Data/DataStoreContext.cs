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

            //one to ine

            //modelBuilder.Entity<User>()//product TO Quantity
            //    .HasOne<Cart>(pt => pt.cart)
            //    .WithOne(p => p.Customer)
            //    .HasForeignKey<Cart>(pt => pt.Id);

            //--------Quantities--------
            //many to many - Quantity table
            modelBuilder.Entity<ProductsQuantity>()
                .HasKey(t => new { t.ProdId, t.ColorId, t.SizeId});

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

           //modelBuilder.Entity<ProductsQuantity>()//cart to ProductsQuantity
           //    .HasOne(pt => pt.cart)
           //    .WithMany(t => t.productList)
           //    .HasForeignKey(pt => pt.CartId);



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



            //Many to many - Product images
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


            //-----Cart----------------
            modelBuilder.Entity<Cart>()
               .HasKey(a => new { a.UserId, a.ProdId, a.ColorId, a.SizeId });

            modelBuilder.Entity<Cart>()//product TO CartItems
                .HasOne(pt => pt.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(pt => pt.ProdId);

            modelBuilder.Entity<Cart>()//productColor TO CartItems
               .HasOne(pt => pt.ProductColor)
               .WithMany(p => p.CartItems)
               .HasForeignKey(pt => pt.ColorId);

            modelBuilder.Entity<Cart>()//productSize TO CartItems
                .HasOne(pt => pt.ProductSize)
                .WithMany(t => t.CartItems)
                .HasForeignKey(pt => pt.SizeId);

            modelBuilder.Entity<Cart>()//User TO CartItems
               .HasOne(pt => pt.User)
               .WithMany(t => t.CartItems)
               .HasForeignKey(pt => pt.UserId);
        }


        public DbSet<MVC_Project.Models.User> User { get; set; }
        public DbSet<MVC_Project.Models.Product> Product { get; set; }
        public DbSet<MVC_Project.Models.ProductCategory> ProductCategory { get; set; }
        public DbSet<MVC_Project.Models.ProductsQuantity> ProductsQuantity { get; set; }
        public DbSet<MVC_Project.Models.ProductSize> ProductSize { get; set; }
        public DbSet<MVC_Project.Models.ProductColor> ProductColor { get; set; }
        public DbSet<MVC_Project.Models.ProductsImages> ProductsImages { get; set; }
        public DbSet<MVC_Project.Models.Cart> Cart { get; set; }
        public DbSet<MVC_Project.Models.Address> Address { get; set; }
    }
}
