using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class relacao_campeonato_escalao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campeonato_Campeonato_campeonatoId",
                table: "Campeonato");

            migrationBuilder.RenameColumn(
                name: "campeonatoId",
                table: "Campeonato",
                newName: "escalaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Campeonato_campeonatoId",
                table: "Campeonato",
                newName: "IX_Campeonato_escalaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campeonato_Escalao_escalaoId",
                table: "Campeonato",
                column: "escalaoId",
                principalTable: "Escalao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campeonato_Escalao_escalaoId",
                table: "Campeonato");

            migrationBuilder.RenameColumn(
                name: "escalaoId",
                table: "Campeonato",
                newName: "campeonatoId");

            migrationBuilder.RenameIndex(
                name: "IX_Campeonato_escalaoId",
                table: "Campeonato",
                newName: "IX_Campeonato_campeonatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campeonato_Campeonato_campeonatoId",
                table: "Campeonato",
                column: "campeonatoId",
                principalTable: "Campeonato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
