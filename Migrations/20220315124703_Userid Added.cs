using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodKoshh.Migrations
{
    public partial class UseridAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Added_Date",
                table: "Seekers");

            migrationBuilder.DropColumn(
                name: "Confirmpwd",
                table: "Seekers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Seekers");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Seekers");

            migrationBuilder.DropColumn(
                name: "Pwd",
                table: "Seekers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Seekers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "UserId",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "bloodKoshhUserId",
                table: "Donors");

            migrationBuilder.AddColumn<DateTime>(
                name: "Added_Date",
                table: "Seekers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Confirmpwd",
                table: "Seekers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Seekers",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Seekers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pwd",
                table: "Seekers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Seekers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
