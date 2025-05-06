using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addmachinetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    machineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    machineName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.machineId);
                });

            migrationBuilder.CreateTable(
                name: "PondIds",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pondId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    machineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PondIds", x => x.id);
                    table.ForeignKey(
                        name: "FK_PondIds_Machines_machineId",
                        column: x => x.machineId,
                        principalTable: "Machines",
                        principalColumn: "machineId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PondIds_machineId",
                table: "PondIds",
                column: "machineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PondIds");

            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
