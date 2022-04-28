using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class User_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UtilizadorFK",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UtilizadorFK",
                table: "AspNetUsers");
        }
    }
}
