using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class abd1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "temperatureTop",
                table: "Configuration",
                newName: "TemperatureTop");

            migrationBuilder.RenameColumn(
                name: "temperatureLow",
                table: "Configuration",
                newName: "TemperatureLow");

            migrationBuilder.RenameColumn(
                name: "oxiTop",
                table: "Configuration",
                newName: "OxiTop");

            migrationBuilder.RenameColumn(
                name: "oxiLow",
                table: "Configuration",
                newName: "OxiLow");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TemperatureTop",
                table: "Configuration",
                newName: "temperatureTop");

            migrationBuilder.RenameColumn(
                name: "TemperatureLow",
                table: "Configuration",
                newName: "temperatureLow");

            migrationBuilder.RenameColumn(
                name: "OxiTop",
                table: "Configuration",
                newName: "oxiTop");

            migrationBuilder.RenameColumn(
                name: "OxiLow",
                table: "Configuration",
                newName: "oxiLow");
        }
    }
}
