using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodKoshh.Migrations
{
    public partial class UserChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BloodBanks_bloodbankid",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Donors_donorid",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Seekers_seekerid",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_bloodbankid",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_donorid",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_seekerid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "bloodbankid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "donorid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "seekerid",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "bloodKoshhUserId",
                table: "Donors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donors_bloodKoshhUserId",
                table: "Donors",
                column: "bloodKoshhUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_AspNetUsers_bloodKoshhUserId",
                table: "Donors",
                column: "bloodKoshhUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_AspNetUsers_bloodKoshhUserId",
                table: "Donors");

            migrationBuilder.DropIndex(
                name: "IX_Donors_bloodKoshhUserId",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "bloodKoshhUserId",
                table: "Donors");

            migrationBuilder.AddColumn<int>(
                name: "bloodbankid",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "donorid",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "seekerid",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_bloodbankid",
                table: "AspNetUsers",
                column: "bloodbankid");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_donorid",
                table: "AspNetUsers",
                column: "donorid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_seekerid",
                table: "AspNetUsers",
                column: "seekerid");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BloodBanks_bloodbankid",
                table: "AspNetUsers",
                column: "bloodbankid",
                principalTable: "BloodBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Donors_donorid",
                table: "AspNetUsers",
                column: "donorid",
                principalTable: "Donors",
                principalColumn: "Donor_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Seekers_seekerid",
                table: "AspNetUsers",
                column: "seekerid",
                principalTable: "Seekers",
                principalColumn: "Seeker_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
