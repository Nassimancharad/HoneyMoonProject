using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HoneyMoonDB.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefoonnummer",
                table: "Afspraak");

            migrationBuilder.AddColumn<string>(
                name: "HerhaalEmail",
                table: "Afspraak",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TelNr",
                table: "Afspraak",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Naam",
                table: "Afspraak",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Afspraak",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HerhaalEmail",
                table: "Afspraak");

            migrationBuilder.DropColumn(
                name: "TelNr",
                table: "Afspraak");

            migrationBuilder.AddColumn<int>(
                name: "Telefoonnummer",
                table: "Afspraak",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Naam",
                table: "Afspraak",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Afspraak",
                nullable: true);
        }
    }
}
