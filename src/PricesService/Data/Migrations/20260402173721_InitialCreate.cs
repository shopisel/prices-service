using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PricesService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "prices",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar", nullable: false),
                    product_id = table.Column<string>(type: "varchar", nullable: false),
                    store_id = table.Column<string>(type: "varchar", nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prices", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_prices_product_id",
                table: "prices",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "UX_prices_product_store",
                table: "prices",
                columns: new[] { "product_id", "store_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prices");
        }
    }
}
