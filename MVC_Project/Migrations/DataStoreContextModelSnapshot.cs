﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MVC_Project.Models;
using System;

namespace MVC_Project.Migrations
{
    [DbContext(typeof(DataStoreContext))]
    partial class DataStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVC_Project.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BuildNumber");

                    b.Property<string>("City");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("MVC_Project.Models.OrdersHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("OrderDate");

                    b.Property<int?>("OrderStatusId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("OrdersHistory");
                });

            modelBuilder.Entity("MVC_Project.Models.OrdersHistoryProductsList", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("ProdId");

                    b.Property<int>("ColorId");

                    b.Property<int>("SizeId");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderId", "ProdId", "ColorId", "SizeId");

                    b.HasIndex("ColorId");

                    b.HasIndex("ProdId");

                    b.HasIndex("SizeId");

                    b.ToTable("OrdersHistoryProductsList");
                });

            modelBuilder.Entity("MVC_Project.Models.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("MVC_Project.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<double>("DiscountPct");

                    b.Property<bool>("IsTradable");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("MVC_Project.Models.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("MVC_Project.Models.ProductColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("RGB");

                    b.HasKey("Id");

                    b.ToTable("ProductColor");
                });

            modelBuilder.Entity("MVC_Project.Models.ProductsImages", b =>
                {
                    b.Property<int>("ProdId");

                    b.Property<int>("ColorId");

                    b.Property<string>("ImgSrc");

                    b.HasKey("ProdId", "ColorId");

                    b.HasIndex("ColorId");

                    b.ToTable("ProductsImages");
                });

            modelBuilder.Entity("MVC_Project.Models.ProductSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Size");

                    b.HasKey("Id");

                    b.ToTable("ProductSize");
                });

            modelBuilder.Entity("MVC_Project.Models.ProductsQuantity", b =>
                {
                    b.Property<int>("ProdId");

                    b.Property<int>("ColorId");

                    b.Property<int>("SizeId");

                    b.Property<int>("Quantity");

                    b.HasKey("ProdId", "ColorId", "SizeId");

                    b.HasIndex("ColorId");

                    b.HasIndex("SizeId");

                    b.ToTable("ProductsQuantity");
                });

            modelBuilder.Entity("MVC_Project.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("NickName");

                    b.Property<string>("Password");

                    b.Property<double>("PhoneNumber");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserType");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MVC_Project.Models.OrdersHistory", b =>
                {
                    b.HasOne("MVC_Project.Models.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId");
                });

            modelBuilder.Entity("MVC_Project.Models.OrdersHistoryProductsList", b =>
                {
                    b.HasOne("MVC_Project.Models.ProductColor", "ProductColor")
                        .WithMany("Orders")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVC_Project.Models.OrdersHistory", "OrdersHistory")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVC_Project.Models.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProdId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVC_Project.Models.ProductSize", "ProductSize")
                        .WithMany("Orders")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVC_Project.Models.Product", b =>
                {
                    b.HasOne("MVC_Project.Models.ProductCategory", "Category")
                        .WithMany("ProductsList")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVC_Project.Models.ProductsImages", b =>
                {
                    b.HasOne("MVC_Project.Models.ProductColor", "ItemColor")
                        .WithMany("Images")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVC_Project.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProdId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVC_Project.Models.ProductsQuantity", b =>
                {
                    b.HasOne("MVC_Project.Models.ProductColor", "ProductColor")
                        .WithMany("Quantity")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVC_Project.Models.Product", "Product")
                        .WithMany("Quantity")
                        .HasForeignKey("ProdId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MVC_Project.Models.ProductSize", "ProductSize")
                        .WithMany("Quantity")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MVC_Project.Models.User", b =>
                {
                    b.HasOne("MVC_Project.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });
#pragma warning restore 612, 618
        }
    }
}
