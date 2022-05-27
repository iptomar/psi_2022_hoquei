using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class duartejoao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubeJogador",
                columns: table => new
                {
                    ListaDeClubesId = table.Column<int>(type: "int", nullable: false),
                    ListaDeJogadoresNum_Fed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubeJogador", x => new { x.ListaDeClubesId, x.ListaDeJogadoresNum_Fed });
                    table.ForeignKey(
                        name: "FK_ClubeJogador_Clube_ListaDeClubesId",
                        column: x => x.ListaDeClubesId,
                        principalTable: "Clube",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubeJogador_Jogador_ListaDeJogadoresNum_Fed",
                        column: x => x.ListaDeJogadoresNum_Fed,
                        principalTable: "Jogador",
                        principalColumn: "Num_Fed",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubeJogador_ListaDeJogadoresNum_Fed",
                table: "ClubeJogador",
                column: "ListaDeJogadoresNum_Fed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubeJogador");
        }
    }
}
