using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodKoshh.Migrations
{
    public partial class modelchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApprovedStatus",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequestStatus",
                table: "Donors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedStatus",
                table: "BloodBanks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequestStatus",
                table: "BloodBanks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedStatus",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "ApprovedStatus",
                table: "BloodBanks");

            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "BloodBanks");
        }
    }
}
