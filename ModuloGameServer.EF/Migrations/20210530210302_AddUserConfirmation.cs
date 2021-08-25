using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddUserConfirmation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommonRating",
                table: "DynamicUserInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserConfirmations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    ConfirmationType = table.Column<int>(type: "int", nullable: false),
                    ConfirmationMethod = table.Column<int>(type: "int", nullable: false),
                    SendStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfirmations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConfirmations");

            migrationBuilder.DropColumn(
                name: "CommonRating",
                table: "DynamicUserInfo");
        }
    }
}
