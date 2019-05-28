using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MVC_Project.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTradable",
                table: "Product",
                newName: "IsTradable");

            migrationBuilder.AddColumn<double>(
                name: "DiscountPct",
                table: "Product",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPct",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "IsTradable",
                table: "Product",
                newName: "IsTradable");
        }
    }
}
