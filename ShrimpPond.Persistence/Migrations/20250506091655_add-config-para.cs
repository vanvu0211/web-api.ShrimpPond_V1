using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addconfigpara : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TopValue = table.Column<float>(type: "real", nullable: false),
                    BottomValue = table.Column<float>(type: "real", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configuration_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "FarmId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_FarmId",
                table: "Configuration",
                column: "FarmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuration");
        }
    }
}
