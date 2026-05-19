using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PricesService.Data.Migrations
{
    /// <inheritdoc />
    public partial class PriceTextFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "prices",
                newName: "price_text");

            migrationBuilder.Sql(
                """
                ALTER TABLE prices
                ALTER COLUMN price_text TYPE varchar
                USING price_text::text;
                """);

            migrationBuilder.RenameColumn(
                name: "sale",
                table: "prices",
                newName: "sale_text");

            migrationBuilder.Sql(
                """
                ALTER TABLE prices
                ALTER COLUMN sale_text TYPE varchar
                USING sale_text::text;
                """);

            migrationBuilder.AddColumn<string>(
                name: "quantity_text",
                table: "prices",
                type: "varchar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "unit_price_text",
                table: "prices",
                type: "varchar",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_prices_products_product_id",
                table: "prices",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_prices_products_product_id",
                table: "prices");

            migrationBuilder.DropColumn(
                name: "quantity_text",
                table: "prices");

            migrationBuilder.DropColumn(
                name: "unit_price_text",
                table: "prices");

            migrationBuilder.Sql(
                """
                ALTER TABLE prices
                ALTER COLUMN price_text TYPE numeric(10,2)
                USING NULLIF(price_text, '')::numeric;
                """);

            migrationBuilder.RenameColumn(
                name: "price_text",
                table: "prices",
                newName: "price");

            migrationBuilder.Sql(
                """
                ALTER TABLE prices
                ALTER COLUMN sale_text TYPE numeric(10,2)
                USING sale_text::numeric;
                """);

            migrationBuilder.RenameColumn(
                name: "sale_text",
                table: "prices",
                newName: "sale");
        }
    }
}
