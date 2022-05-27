using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class jogosv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JogoId",
                table: "Jogador",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JogoId1",
                table: "Jogador",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Jogo",
                columns: table => new
                {
                    JogoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Clube_CasaId = table.Column<int>(type: "int", nullable: true),
                    Clube_ForaId = table.Column<int>(type: "int", nullable: true),
                    Escalao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GolosCasa = table.Column<int>(type: "int", nullable: false),
                    GolosFora = table.Column<int>(type: "int", nullable: false),
                    Capitao_CasaNum_Fed = table.Column<int>(type: "int", nullable: true),
                    Capitao_ForaNum_Fed = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogo", x => x.JogoId);
                    table.ForeignKey(
                        name: "FK_Jogo_Jogador_Capitao_CasaNum_Fed",
                        column: x => x.Capitao_CasaNum_Fed,
                        principalTable: "Jogador",
                        principalColumn: "Num_Fed",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jogo_Jogador_Capitao_ForaNum_Fed",
                        column: x => x.Capitao_ForaNum_Fed,
                        principalTable: "Jogador",
                        principalColumn: "Num_Fed",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clube",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Fundacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JogoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clube", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clube_Jogo_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogo",
                        principalColumn: "JogoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_JogoId",
                table: "Jogador",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_JogoId1",
                table: "Jogador",
                column: "JogoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Clube_JogoId",
                table: "Clube",
                column: "JogoId");

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
                name: "FK_Jogador_Jogo_JogoId",
                table: "Jogador",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Jogo_JogoId",
                table: "Jogador");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Jogo_JogoId1",
                table: "Jogador");

            migrationBuilder.DropForeignKey(
                name: "FK_Clube_Jogo_JogoId",
                table: "Clube");

            migrationBuilder.DropTable(
                name: "Jogo");

            migrationBuilder.DropTable(
                name: "Clube");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_JogoId",
                table: "Jogador");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_JogoId1",
                table: "Jogador");

            migrationBuilder.DropColumn(
                name: "JogoId",
                table: "Jogador");

            migrationBuilder.DropColumn(
                name: "JogoId1",
                table: "Jogador");
        }
    }
}
