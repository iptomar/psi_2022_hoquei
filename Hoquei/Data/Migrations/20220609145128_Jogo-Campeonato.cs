using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class JogoCampeonato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampeonatoId",
                table: "Jogo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_CampeonatoId",
                table: "Jogo",
                column: "CampeonatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Campeonato_CampeonatoId",
                table: "Jogo",
                column: "CampeonatoId",
                principalTable: "Campeonato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Campeonato_CampeonatoId",
                table: "Jogo");

            migrationBuilder.DropIndex(
                name: "IX_Jogo_CampeonatoId",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "CampeonatoId",
                table: "Jogo");
        }
    }
}
