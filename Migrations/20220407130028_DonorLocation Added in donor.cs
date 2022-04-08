using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodKoshh.Migrations
{
    public partial class DonorLocationAddedindonor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Donors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Donors");
        }
    }
}
