using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addemaildb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "FarmRoles");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "FarmRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "FarmRoles");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "FarmRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
