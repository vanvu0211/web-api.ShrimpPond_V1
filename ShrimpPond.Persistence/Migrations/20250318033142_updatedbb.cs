using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class updatedbb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "enableFarm",
                table: "TimeSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "enableFarm",
                table: "TimeSettings");
        }
    }
}
