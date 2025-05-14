using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class updatefarm1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FarmRoles_Farms_FarmId1",
                table: "FarmRoles");

            migrationBuilder.DropIndex(
                name: "IX_FarmRoles_FarmId1",
                table: "FarmRoles");

            migrationBuilder.DropColumn(
                name: "FarmId1",
                table: "FarmRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmId1",
                table: "FarmRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FarmRoles_FarmId1",
                table: "FarmRoles",
                column: "FarmId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FarmRoles_Farms_FarmId1",
                table: "FarmRoles",
                column: "FarmId1",
                principalTable: "Farms",
                principalColumn: "FarmId");
        }
    }
}
