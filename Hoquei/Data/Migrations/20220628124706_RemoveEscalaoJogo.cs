using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class RemoveEscalaoJogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Escalao",
                table: "Jogo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Escalao",
                table: "Jogo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
