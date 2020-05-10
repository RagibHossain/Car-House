using Microsoft.EntityFrameworkCore.Migrations;

namespace Car_House.Migrations
{
    public partial class CarDisplayImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayImage",
                table: "Cars",
                nullable: true,
                defaultValue: "red.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayImage",
                table: "Cars");
        }
    }
}
