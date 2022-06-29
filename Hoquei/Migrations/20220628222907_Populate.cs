using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Migrations
{
    public partial class Populate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Campeonato",
                columns: new[] { "Id", "Designacao", "escalaoId" },
                values: new object[] { 1, "SuperLiga", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Campeonato",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a",
                column: "ConcurrencyStamp",
                value: "52a4f92d-c50f-4cd5-88e4-1f313d66bbe9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "u",
                column: "ConcurrencyStamp",
                value: "ea86ec2e-8798-476d-9957-b2b1d0a1fd50");
        }
    }
}
