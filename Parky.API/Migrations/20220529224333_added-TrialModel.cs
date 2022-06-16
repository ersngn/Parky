using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Parky.API.Migrations
{
    public partial class addedTrialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_nationalParks",
                table: "nationalParks");

            migrationBuilder.RenameTable(
                name: "nationalParks",
                newName: "NationalParks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NationalParks",
                table: "NationalParks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Trials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    NationalParkId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isOpen = table.Column<bool>(type: "bit", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trials", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NationalParks",
                table: "NationalParks");

            migrationBuilder.RenameTable(
                name: "NationalParks",
                newName: "nationalParks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_nationalParks",
                table: "nationalParks",
                column: "Id");
        }
    }
}
