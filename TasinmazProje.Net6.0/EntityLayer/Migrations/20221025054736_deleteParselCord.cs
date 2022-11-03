using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreLayer.Migrations
{
    public partial class deleteParselCord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParselCoordinate",
                table: "Tasinmazs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParselCoordinate",
                table: "Tasinmazs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
