using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class jogov6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capitao_Casa",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "Capitao_Fora",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "Clube_Casa",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "Clube_Fora",
                table: "Jogo");

            migrationBuilder.AddColumn<int>(
                name: "Capitao_CasaNum_Fed",
                table: "Jogo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capitao_ForaNum_Fed",
                table: "Jogo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Clube_CasaId",
                table: "Jogo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Clube_ForaId",
                table: "Jogo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_Capitao_CasaNum_Fed",
                table: "Jogo",
                column: "Capitao_CasaNum_Fed");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_Capitao_ForaNum_Fed",
                table: "Jogo",
                column: "Capitao_ForaNum_Fed");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_Clube_CasaId",
                table: "Jogo",
                column: "Clube_CasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_Clube_ForaId",
                table: "Jogo",
                column: "Clube_ForaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Clube_Clube_CasaId",
                table: "Jogo",
                column: "Clube_CasaId",
                principalTable: "Clube",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Clube_Clube_ForaId",
                table: "Jogo",
                column: "Clube_ForaId",
                principalTable: "Clube",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Jogador_Capitao_CasaNum_Fed",
                table: "Jogo",
                column: "Capitao_CasaNum_Fed",
                principalTable: "Jogador",
                principalColumn: "Num_Fed",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Jogador_Capitao_ForaNum_Fed",
                table: "Jogo",
                column: "Capitao_ForaNum_Fed",
                principalTable: "Jogador",
                principalColumn: "Num_Fed",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Clube_Clube_CasaId",
                table: "Jogo");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Clube_Clube_ForaId",
                table: "Jogo");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Jogador_Capitao_CasaNum_Fed",
                table: "Jogo");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Jogador_Capitao_ForaNum_Fed",
                table: "Jogo");

            migrationBuilder.DropIndex(
                name: "IX_Jogo_Capitao_CasaNum_Fed",
                table: "Jogo");

            migrationBuilder.DropIndex(
                name: "IX_Jogo_Capitao_ForaNum_Fed",
                table: "Jogo");

            migrationBuilder.DropIndex(
                name: "IX_Jogo_Clube_CasaId",
                table: "Jogo");

            migrationBuilder.DropIndex(
                name: "IX_Jogo_Clube_ForaId",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "Capitao_CasaNum_Fed",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "Capitao_ForaNum_Fed",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "Clube_CasaId",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "Clube_ForaId",
                table: "Jogo");

            migrationBuilder.AddColumn<int>(
                name: "Capitao_Casa",
                table: "Jogo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capitao_Fora",
                table: "Jogo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Clube_Casa",
                table: "Jogo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Clube_Fora",
                table: "Jogo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
