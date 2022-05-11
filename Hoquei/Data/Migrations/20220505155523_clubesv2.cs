using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class clubesv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clube",
                table: "Clube");

            migrationBuilder.RenameTable(
                name: "Clube",
                newName: "ListaDeClubes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListaDeClubes",
                table: "ListaDeClubes",
                column: "Id");

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
                        name: "FK_ClubeJogador_Jogador_ListaDeJogadoresNum_Fed",
                        column: x => x.ListaDeJogadoresNum_Fed,
                        principalTable: "Jogador",
                        principalColumn: "Num_Fed",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubeJogador_ListaDeClubes_ListaDeClubesId",
                        column: x => x.ListaDeClubesId,
                        principalTable: "ListaDeClubes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jogador",
                columns: new[] { "Num_Fed", "Alcunha", "Data_Nasc", "Foto", "Name", "Num_Cam" },
                values: new object[] { 1, "Toni", new DateTime(2000, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cristiano_Ronaldo_2018.jpg", "Antonio Alberto", 10 });

            migrationBuilder.CreateIndex(
                name: "IX_ClubeJogador_ListaDeJogadoresNum_Fed",
                table: "ClubeJogador",
                column: "ListaDeJogadoresNum_Fed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubeJogador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListaDeClubes",
                table: "ListaDeClubes");

            migrationBuilder.DeleteData(
                table: "Jogador",
                keyColumn: "Num_Fed",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "ListaDeClubes",
                newName: "Clube");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clube",
                table: "Clube",
                column: "Id");
        }
    }
}
