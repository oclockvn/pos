using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos.core.Migrations
{
    public partial class AddCreatedDateToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "product",
                table: "Products",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<string>(
                name: "CreatedId",
                schema: "product",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                schema: "product",
                table: "Products",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedId",
                schema: "product",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedId",
                schema: "product",
                table: "Products");
        }
    }
}
