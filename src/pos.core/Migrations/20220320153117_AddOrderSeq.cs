using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pos.core.Migrations
{
    public partial class AddOrderSeq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "product",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                schema: "product",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeightUnit",
                schema: "product",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.Sql(@"if not exists (select * FROM sys.sequences WHERE name = 'order_seq') CREATE SEQUENCE order_seq
                  AS BIGINT
                  START WITH 1
                  INCREMENT BY 1
                  MINVALUE 1
                  MAXVALUE 999999; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WeightUnit",
                schema: "product",
                table: "Products");
        }
    }
}
