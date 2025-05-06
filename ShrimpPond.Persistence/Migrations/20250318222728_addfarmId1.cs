using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addfarmId1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "farmId",
                table: "Pond");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "farmId",
                table: "Pond",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
