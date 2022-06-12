using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos.core.Migrations
{
    public partial class UpdateProductCategoryAndBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WholesalesPrice",
                schema: "product",
                table: "Products",
                newName: "WholesalePrice");

            migrationBuilder.RenameColumn(
                name: "SalesPrice",
                schema: "product",
                table: "Products",
                newName: "SalePrice");

            migrationBuilder.AddColumn<long>(
                name: "BrandId",
                schema: "product",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                schema: "product",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                schema: "product",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Sellable",
                schema: "product",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Taxable",
                schema: "product",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                schema: "product",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: "product",
                table: "Products",
                column: "CategoryId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brand_BrandId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductType",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sellable",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Taxable",
                schema: "product",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "WholesalePrice",
                schema: "product",
                table: "Products",
                newName: "WholesalesPrice");

            migrationBuilder.RenameColumn(
                name: "SalePrice",
                schema: "product",
                table: "Products",
                newName: "SalesPrice");
        }
    }
}
