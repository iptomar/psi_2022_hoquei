using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class calssv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampeonatoId",
                table: "Jogo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CampeonatoId",
                table: "Clube",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Classificacoes",
                columns: table => new
                {
                    Id_TabelaDeClassificacoes = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Campeonato_IdId = table.Column<int>(type: "int", nullable: true),
                    ClubeId = table.Column<int>(type: "int", nullable: true),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    Golos_Marcados = table.Column<int>(type: "int", nullable: false),
                    Golos_Sofridos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classificacoes", x => x.Id_TabelaDeClassificacoes);
                    table.ForeignKey(
                        name: "FK_Classificacoes_Campeonato_Campeonato_IdId",
                        column: x => x.Campeonato_IdId,
                        principalTable: "Campeonato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classificacoes_Clube_ClubeId",
                        column: x => x.ClubeId,
                        principalTable: "Clube",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogo_CampeonatoId",
                table: "Jogo",
                column: "CampeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clube_CampeonatoId",
                table: "Clube",
                column: "CampeonatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Classificacoes_Campeonato_IdId",
                table: "Classificacoes",
                column: "Campeonato_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Classificacoes_ClubeId",
                table: "Classificacoes",
                column: "ClubeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clube_Campeonato_CampeonatoId",
                table: "Clube",
                column: "CampeonatoId",
                principalTable: "Campeonato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Campeonato_CampeonatoId",
                table: "Jogo",
                column: "CampeonatoId",
                principalTable: "Campeonato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clube_Campeonato_CampeonatoId",
                table: "Clube");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Campeonato_CampeonatoId",
                table: "Jogo");

            migrationBuilder.DropTable(
                name: "Classificacoes");

            migrationBuilder.DropIndex(
                name: "IX_Jogo_CampeonatoId",
                table: "Jogo");

            migrationBuilder.DropIndex(
                name: "IX_Clube_CampeonatoId",
                table: "Clube");

            migrationBuilder.DropColumn(
                name: "CampeonatoId",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "CampeonatoId",
                table: "Clube");
        }
    }
}
