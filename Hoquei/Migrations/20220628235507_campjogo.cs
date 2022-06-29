using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Migrations
{
    public partial class campjogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Campeonato_CampeonatosId",
                table: "Jogo");

            migrationBuilder.DeleteData(
                table: "Escalao",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Escalao",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Escalao",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Escalao",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Escalao",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<int>(
                name: "CampeonatosId",
                table: "Jogo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "ee6c131d-2106-4683-a275-3d936a50cb38");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "2b2687d5-1154-46ef-9d4d-671d70ebf633");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Campeonato_CampeonatosId",
                table: "Jogo",
                column: "CampeonatosId",
                principalTable: "Campeonato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Campeonato_CampeonatosId",
                table: "Jogo");

            migrationBuilder.AlterColumn<int>(
                name: "CampeonatosId",
                table: "Jogo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "abbec2e8-aa14-4f48-863b-f755b6a48419");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "ea6ed5bd-5bfb-4edb-8eaa-74e385412ea0");

            migrationBuilder.InsertData(
                table: "Escalao",
                columns: new[] { "Id", "designacao" },
                values: new object[,]
                {
                    { 1, "Infantis" },
                    { 2, "Iniciados" },
                    { 3, "Juvenis" },
                    { 4, "Juniores" },
                    { 5, "Seniores" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Jogo_Campeonato_CampeonatosId",
                table: "Jogo",
                column: "CampeonatosId",
                principalTable: "Campeonato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
