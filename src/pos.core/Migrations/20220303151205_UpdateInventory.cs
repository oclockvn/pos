using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos.core.Migrations
{
    public partial class UpdateInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "product",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "product",
                table: "Inventories",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedId",
                schema: "product",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                schema: "product",
                table: "Inventories",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedId",
                schema: "product",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "product",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CreatedId",
                schema: "product",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "product",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "UpdatedId",
                schema: "product",
                table: "Inventories");
        }
    }
}
