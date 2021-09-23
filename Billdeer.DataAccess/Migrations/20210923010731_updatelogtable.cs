using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Billdeer.DataAccess.Migrations
{
    public partial class updatelogtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Logs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Logs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Logs",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Logs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Logs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Logs",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
