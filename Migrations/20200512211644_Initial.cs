using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Car_House.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    SecurityQuestion1 = table.Column<string>(nullable: false),
                    SecurityQuestion2 = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true, defaultValue: "Person.jpg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarID = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: false),
                    BrandID = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: true, defaultValue: "none"),
                    Transmission = table.Column<int>(nullable: false),
                    Condition = table.Column<int>(nullable: false),
                    FuelType = table.Column<int>(nullable: false),
                    GearType = table.Column<int>(nullable: false),
                    BodyType = table.Column<string>(nullable: false),
                    EngineSize = table.Column<string>(nullable: true),
                    NoOfSeats = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Mileage = table.Column<decimal>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    DisplayImage = table.Column<string>(nullable: true, defaultValue: "red.jpg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarID);
                    table.ForeignKey(
                        name: "FK_Cars_Brands_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brands",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageLocation = table.Column<string>(nullable: true),
                    CarID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_Images_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandID",
                table: "Cars",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CarID",
                table: "Images",
                column: "CarID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
