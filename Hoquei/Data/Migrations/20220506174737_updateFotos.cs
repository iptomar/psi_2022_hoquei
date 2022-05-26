using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class updateFotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Jogador",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
