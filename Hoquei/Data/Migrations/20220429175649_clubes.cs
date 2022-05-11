using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class clubes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clube",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Fundacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FotografiasID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clube", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Clube",
                columns: new[] { "Id", "Data_Fundacao", "FotografiasID", "Nome" },
                values: new object[] { 1, new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "grey-material-design-4k-7l.jpg", "Marisanhense HC" });

            migrationBuilder.InsertData(
                table: "Clube",
                columns: new[] { "Id", "Data_Fundacao", "FotografiasID", "Nome" },
                values: new object[] { 2, new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "grey-material-design-4k-7l.jpg", "Fátimanense HC" });

            migrationBuilder.InsertData(
                table: "Clube",
                columns: new[] { "Id", "Data_Fundacao", "FotografiasID", "Nome" },
                values: new object[] { 3, new DateTime(2018, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "grey-material-design-4k-7l.jpg", "RoPinto HC" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clube");
        }
    }
}
