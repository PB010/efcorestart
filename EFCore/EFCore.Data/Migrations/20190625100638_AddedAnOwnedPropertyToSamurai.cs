﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.Data.Migrations
{
    public partial class AddedAnOwnedPropertyToSamurai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BetterName_Created",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BetterName_GivenName",
                table: "Samurais",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BetterName_LastModified",
                table: "Samurais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BetterName_Surname",
                table: "Samurais",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetterName_Created",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "BetterName_GivenName",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "BetterName_LastModified",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "BetterName_Surname",
                table: "Samurais");
        }
    }
}
