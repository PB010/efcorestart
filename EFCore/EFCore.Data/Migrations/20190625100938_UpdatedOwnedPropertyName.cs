using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Data.Migrations
{
    public partial class UpdatedOwnedPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BetterName_Surname",
                table: "Samurais",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "BetterName_GivenName",
                table: "Samurais",
                newName: "GivenName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Samurais",
                newName: "BetterName_Surname");

            migrationBuilder.RenameColumn(
                name: "GivenName",
                table: "Samurais",
                newName: "BetterName_GivenName");
        }
    }
}
