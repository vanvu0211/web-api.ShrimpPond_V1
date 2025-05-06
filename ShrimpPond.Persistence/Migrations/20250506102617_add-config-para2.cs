using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addconfigpara2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Configuration");

            migrationBuilder.RenameColumn(
                name: "TopValue",
                table: "Configuration",
                newName: "temperatureTop");

            migrationBuilder.RenameColumn(
                name: "BottomValue",
                table: "Configuration",
                newName: "temperatureLow");

            migrationBuilder.AddColumn<double>(
                name: "oxiLow",
                table: "Configuration",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "oxiTop",
                table: "Configuration",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "pHLow",
                table: "Configuration",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "pHTop",
                table: "Configuration",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "oxiLow",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "oxiTop",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "pHLow",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "pHTop",
                table: "Configuration");

            migrationBuilder.RenameColumn(
                name: "temperatureTop",
                table: "Configuration",
                newName: "TopValue");

            migrationBuilder.RenameColumn(
                name: "temperatureLow",
                table: "Configuration",
                newName: "BottomValue");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Configuration",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
