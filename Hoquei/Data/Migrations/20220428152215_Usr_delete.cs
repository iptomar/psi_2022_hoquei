using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class Usr_delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "connection",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
