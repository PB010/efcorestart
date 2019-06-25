using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Data.Migrations
{
    public partial class RemovedShadowPropertiesForIsOwned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetterName_Created",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "BetterName_LastModified",
                table: "Samurais");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BetterName_Created",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "BetterName_LastModified",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
