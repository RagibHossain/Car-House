using Microsoft.EntityFrameworkCore.Migrations;

namespace Car_House.Migrations
{
    public partial class OnDeleteCarBehaviorChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cars_CarID",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cars_CarID",
                table: "Images",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "CarID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cars_CarID",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cars_CarID",
                table: "Images",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "CarID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
