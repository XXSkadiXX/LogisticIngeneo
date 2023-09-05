using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Core.Migrations
{
    public partial class updateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Logistic",
                table: "Warehouses",
                newName: "Warehouse");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Logistic",
                table: "Seaports",
                newName: "Seaport");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                schema: "Security",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDate",
                schema: "Security",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Warehouse",
                schema: "Logistic",
                table: "Warehouses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Seaport",
                schema: "Logistic",
                table: "Seaports",
                newName: "Name");
        }
    }
}
