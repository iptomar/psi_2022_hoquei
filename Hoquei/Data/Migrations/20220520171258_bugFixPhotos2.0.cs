using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class bugFixPhotos20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Foto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Foto",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
