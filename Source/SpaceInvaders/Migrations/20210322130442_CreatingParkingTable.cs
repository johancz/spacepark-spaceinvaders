using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceInvaders.Migrations
{
    public partial class CreatingParkingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parkings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Traveller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StarShip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalSum = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parkings");
        }
    }
}
