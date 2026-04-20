using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PricesService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleAndSaleDateToPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "sale",
                table: "prices",
                type: "numeric(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "sale_date",
                table: "prices",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sale",
                table: "prices");

            migrationBuilder.DropColumn(
                name: "sale_date",
                table: "prices");
        }
    }
}
