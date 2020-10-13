using Microsoft.EntityFrameworkCore.Migrations;

namespace Zadatak.Migrations
{
    public partial class PostanskiBrojNaselje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PostanskiBroj",
                table: "Naselja",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PostanskiBroj",
                table: "Naselja",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
