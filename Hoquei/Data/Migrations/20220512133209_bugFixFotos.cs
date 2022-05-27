using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class bugFixFotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foto_Jogador_JogadorId",
                table: "Foto");

            migrationBuilder.DropIndex(
                name: "IX_Foto_JogadorId",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "JogadorId",
                table: "Foto");

            migrationBuilder.AddColumn<int>(
                name: "FotoId",
                table: "Jogador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_FotoId",
                table: "Jogador",
                column: "FotoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogador_Foto_FotoId",
                table: "Jogador",
                column: "FotoId",
                principalTable: "Foto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Foto_FotoId",
                table: "Jogador");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_FotoId",
                table: "Jogador");

            migrationBuilder.DropColumn(
                name: "FotoId",
                table: "Jogador");

            migrationBuilder.AddColumn<int>(
                name: "JogadorId",
                table: "Foto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Foto_JogadorId",
                table: "Foto",
                column: "JogadorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Foto_Jogador_JogadorId",
                table: "Foto",
                column: "JogadorId",
                principalTable: "Jogador",
                principalColumn: "Num_Fed",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
