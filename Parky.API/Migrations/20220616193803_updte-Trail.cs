using Microsoft.EntityFrameworkCore.Migrations;

namespace Parky.API.Migrations
{
    public partial class updteTrail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Trials_NationalParkId",
                table: "Trials",
                column: "NationalParkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trials_NationalParks_NationalParkId",
                table: "Trials",
                column: "NationalParkId",
                principalTable: "NationalParks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trials_NationalParks_NationalParkId",
                table: "Trials");

            migrationBuilder.DropIndex(
                name: "IX_Trials_NationalParkId",
                table: "Trials");
        }
    }
}
