using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addmachinetable12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "machineId",
                table: "PondIds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "machineId1",
                table: "PondIds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PondIds_machineId1",
                table: "PondIds",
                column: "machineId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PondIds_Machines_machineId1",
                table: "PondIds",
                column: "machineId1",
                principalTable: "Machines",
                principalColumn: "machineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PondIds_Machines_machineId1",
                table: "PondIds");

            migrationBuilder.DropIndex(
                name: "IX_PondIds_machineId1",
                table: "PondIds");

            migrationBuilder.DropColumn(
                name: "machineId1",
                table: "PondIds");

            migrationBuilder.AlterColumn<int>(
                name: "machineId",
                table: "PondIds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
