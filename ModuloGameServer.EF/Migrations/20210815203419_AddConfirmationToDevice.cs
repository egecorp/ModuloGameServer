using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddConfirmationToDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifyCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerifyLastRequestStamp",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "LinkCode",
                table: "UserConfirmations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsUser2Bot",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "CurrentConfirmationId",
                table: "Devices",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkCode",
                table: "UserConfirmations");

            migrationBuilder.DropColumn(
                name: "CurrentConfirmationId",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "VerifyCode",
                table: "Users",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifyLastRequestStamp",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsUser2Bot",
                table: "Games",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
