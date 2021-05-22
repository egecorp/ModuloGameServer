using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddPreCalculatedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastRoundStamp",
                table: "Games",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User1MaxRoundNumber",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User1Score",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User2MaxRoundNumber",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "User2Score",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastRoundStamp",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "User1MaxRoundNumber",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "User1Score",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "User2MaxRoundNumber",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "User2Score",
                table: "Games");
        }
    }
}
