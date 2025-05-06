using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addalarmtb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alarm",
                columns: table => new
                {
                    AlarmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlarmName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlarmDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlarmDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    farmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarm", x => x.AlarmId);
                    table.ForeignKey(
                        name: "FK_Alarm_Farms_farmId",
                        column: x => x.farmId,
                        principalTable: "Farms",
                        principalColumn: "farmId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alarm_farmId",
                table: "Alarm",
                column: "farmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarm");
        }
    }
}
