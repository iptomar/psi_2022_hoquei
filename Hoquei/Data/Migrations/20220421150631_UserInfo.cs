using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class UserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumTele = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CC", "DataNascimento", "Email", "Nome", "NumTele", "UserName" },
                values: new object[,]
                {
                    { 1, "098446793", new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marisa.Freitas@iol.pt", "Marisa Vieira", "967197885", "MarVi" },
                    { 2, "098446795", new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fátima.Machado@gmail.com", "Fátima Ribeiro", "963737476", "FáRibeiro" },
                    { 4, "098446801", new DateTime(2011, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paula.Lopes@iol.pt", "Paula Silva", "967517256", "Pauva" },
                    { 5, "098446804", new DateTime(2008, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariline.Martins@sapo.pt", "Mariline Marques", "967212144", "Mariques" },
                    { 6, "098446807", new DateTime(2012, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marcos.Rocha@sapo.pt", "Marcos Rocha", "962125638", "Marcha" },
                    { 7, "098446809", new DateTime(2010, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alexandre.Dias@hotmail.com", "Alexandre Vieira", "961493756", "Alexeira" },
                    { 8, "098446811", new DateTime(2010, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paula.Vieira@clix.pt", "Paula Soares", "961937768", "Paulares" },
                    { 9, "098446799", new DateTime(2017, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mariline.Ribeiro@iol.pt", "Mariline Santos", "964106478", "Marilintos" },
                    { 10, "098446812", new DateTime(2018, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roberto.Vieira@sapo.pt", "Roberto Pinto", "964685937", "RoPinto" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
