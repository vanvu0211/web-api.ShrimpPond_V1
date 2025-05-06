using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addmachinetable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "farmId",
                table: "Machines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Machines_farmId",
                table: "Machines",
                column: "farmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Farms_farmId",
                table: "Machines",
                column: "farmId",
                principalTable: "Farms",
                principalColumn: "farmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Farms_farmId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_farmId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "farmId",
                table: "Machines");
        }
    }
}
