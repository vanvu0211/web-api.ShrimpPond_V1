using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    public partial class addmachinetable1234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Machines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Machines");
        }
    }
}
