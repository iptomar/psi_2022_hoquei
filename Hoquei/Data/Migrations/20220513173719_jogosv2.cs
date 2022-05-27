using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class jogosv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clube_Jogo_JogoId",
                table: "Clube");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Jogo_JogoId1",
                table: "Jogador");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_JogoId1",
                table: "Jogador");

            migrationBuilder.DropIndex(
                name: "IX_Clube_JogoId",
                table: "Clube");

            migrationBuilder.DropColumn(
                name: "JogoId1",
                table: "Jogador");

            migrationBuilder.DropColumn(
                name: "JogoId",
                table: "Clube");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JogoId1",
                table: "Jogador",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JogoId",
                table: "Clube",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_JogoId1",
                table: "Jogador",
                column: "JogoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Clube_JogoId",
                table: "Clube",
                column: "JogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clube_Jogo_JogoId",
                table: "Clube",
                column: "JogoId",
                principalTable: "Jogo",
                principalColumn: "JogoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogador_Jogo_JogoId1",
                table: "Jogador",
                column: "JogoId1",
                principalTable: "Jogo",
                principalColumn: "JogoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
