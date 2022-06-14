using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeCounter.DataLayer.Migrations
{
    public partial class AddEnabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Widgets",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Widgets");
        }
    }
}
