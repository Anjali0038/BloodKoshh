using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodKoshh.Migrations
{
    public partial class DonorAddedinlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Donor_id",
                table: "DonorLocation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonorLocation_Donor_id",
                table: "DonorLocation",
                column: "Donor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorLocation_Donors_Donor_id",
                table: "DonorLocation",
                column: "Donor_id",
                principalTable: "Donors",
                principalColumn: "Donor_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorLocation_Donors_Donor_id",
                table: "DonorLocation");

            migrationBuilder.DropIndex(
                name: "IX_DonorLocation_Donor_id",
                table: "DonorLocation");

            migrationBuilder.DropColumn(
                name: "Donor_id",
                table: "DonorLocation");
        }
    }
}
