using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HoneyMoonDB.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afspraak",
                columns: table => new
                {
                    AfspraakId = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    HerhaalEmail = table.Column<string>(nullable: true),
                    Naam = table.Column<string>(nullable: true),
                    Nieuwsbrief = table.Column<bool>(nullable: false),
                    Telefoonnummer = table.Column<int>(nullable: false),
                    Tijd = table.Column<DateTime>(nullable: false),
                    TrouwDatum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afspraak", x => x.AfspraakId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Afspraak");
        }
    }
}
