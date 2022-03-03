using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos.core.Migrations
{
    public partial class AddUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                schema: "product",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                schema: "product",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("update [product].[products] set [sku] = newid() where COALESCE([sku], '') = ''");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                schema: "product",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Barcode",
                schema: "product",
                table: "Products",
                column: "Barcode",
                unique: true,
                filter: "[Barcode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Sku",
                schema: "product",
                table: "Products",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                schema: "product",
                table: "Orders",
                column: "OrderNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Barcode",
                schema: "product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Sku",
                schema: "product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderNumber",
                schema: "product",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Barcode",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sku",
                schema: "product",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                schema: "product",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
