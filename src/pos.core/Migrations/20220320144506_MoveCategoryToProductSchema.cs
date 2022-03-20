using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos.core.Migrations
{
    public partial class MoveCategoryToProductSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brand_BrandId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories",
                newSchema: "product");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands",
                newSchema: "product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                schema: "product",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                schema: "product",
                table: "Brands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                schema: "product",
                table: "Products",
                column: "BrandId",
                principalSchema: "product",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "product",
                table: "Products",
                column: "CategoryId",
                principalSchema: "product",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                schema: "product",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                schema: "product",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "product",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Brands",
                schema: "product",
                newName: "Brand");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brand_BrandId",
                schema: "product",
                table: "Products",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CategoryId",
                schema: "product",
                table: "Products",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
