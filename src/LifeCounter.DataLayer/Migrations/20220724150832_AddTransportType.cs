using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeCounter.DataLayer.Migrations
{
    public partial class AddTransportType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransportType",
                table: "Widgets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransportType",
                table: "Widgets");
        }
    }
}
