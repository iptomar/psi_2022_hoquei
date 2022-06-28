using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class marcadoresclubes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JogoId1",
                table: "Jogador",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_JogoId1",
                table: "Jogador",
                column: "JogoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogador_Jogo_JogoId1",
                table: "Jogador",
                column: "JogoId1",
                principalTable: "Jogo",
                principalColumn: "JogoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Jogo_JogoId1",
                table: "Jogador");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_JogoId1",
                table: "Jogador");

            migrationBuilder.DropColumn(
                name: "JogoId1",
                table: "Jogador");
        }
    }
}
