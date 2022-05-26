using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class jogov4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_ListaDeClubes_Clube_CasaId",
                table: "Jogo");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_ListaDeClubes_Clube_ForaId",
                table: "Jogo");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaDeClubes_Jogo_JogoId",
                table: "ListaDeClubes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListaDeClubes",
                table: "ListaDeClubes");

            migrationBuilder.DropIndex(
                name: "IX_ListaDeClubes_JogoId",
                table: "ListaDeClubes");

            migrationBuilder.DropColumn(
                name: "JogoId",
                table: "ListaDeClubes");

            migrationBuilder.RenameTable(
                name: "ListaDeClubes",
                newName: "Clube");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clube",
                table: "Clube",
                column: "Id");

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
                name: "FK_Jogo_Clube_Clube_CasaId",
                table: "Jogo");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Clube_Clube_ForaId",
                table: "Jogo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clube",
                table: "Clube");

            migrationBuilder.RenameTable(
                name: "Clube",
                newName: "ListaDeClubes");

            migrationBuilder.AddColumn<int>(
                name: "JogoId",
                table: "ListaDeClubes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListaDeClubes",
                table: "ListaDeClubes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ListaDeClubes_JogoId",
                table: "ListaDeClubes",
                column: "JogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_ListaDeClubes_Clube_CasaId",
                table: "Jogo",
                column: "Clube_CasaId",
                principalTable: "ListaDeClubes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_ListaDeClubes_Clube_ForaId",
                table: "Jogo",
                column: "Clube_ForaId",
                principalTable: "ListaDeClubes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaDeClubes_Jogo_JogoId",
                table: "ListaDeClubes",
                column: "JogoId",
                principalTable: "Jogo",
                principalColumn: "JogoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
