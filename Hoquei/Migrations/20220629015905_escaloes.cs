using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Migrations
{
    public partial class escaloes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "02c28b60-4c6e-4eed-b119-d09a6b22ac7c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "4e50dc3a-28ff-4f66-80da-26d82e321981");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
