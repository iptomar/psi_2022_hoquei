using Microsoft.EntityFrameworkCore.Migrations;

namespace Hoquei.Data.Migrations
{
    public partial class addFotosClube : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Clube");

            migrationBuilder.AddColumn<int>(
                name: "FotoId",
                table: "Clube",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clube_FotoId",
                table: "Clube",
                column: "FotoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clube_Foto_FotoId",
                table: "Clube",
                column: "FotoId",
                principalTable: "Foto",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clube_Foto_FotoId",
                table: "Clube");

            migrationBuilder.DropIndex(
                name: "IX_Clube_FotoId",
                table: "Clube");

            migrationBuilder.DropColumn(
                name: "FotoId",
                table: "Clube");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Clube",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
