using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MVC_Project.Migrations
{
    public partial class updateDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdersHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderStatusId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersHistory_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdersHistoryProductsList",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ProdId = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    SizeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersHistoryProductsList", x => new { x.OrderId, x.ProdId, x.ColorId, x.SizeId });
                    table.ForeignKey(
                        name: "FK_OrdersHistoryProductsList_ProductColor_ColorId",
                        column: x => x.ColorId,
                        principalTable: "ProductColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersHistoryProductsList_OrdersHistory_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrdersHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersHistoryProductsList_Product_ProdId",
                        column: x => x.ProdId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersHistoryProductsList_ProductSize_SizeId",
                        column: x => x.SizeId,
                        principalTable: "ProductSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistory_OrderStatusId",
                table: "OrdersHistory",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistoryProductsList_ColorId",
                table: "OrdersHistoryProductsList",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistoryProductsList_ProdId",
                table: "OrdersHistoryProductsList",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersHistoryProductsList_SizeId",
                table: "OrdersHistoryProductsList",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersHistoryProductsList");

            migrationBuilder.DropTable(
                name: "OrdersHistory");

            migrationBuilder.DropTable(
                name: "OrderStatus");
        }
    }
}
