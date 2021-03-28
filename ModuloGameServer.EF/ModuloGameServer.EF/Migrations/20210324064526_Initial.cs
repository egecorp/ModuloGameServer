using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerToken = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NicName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    TNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifyCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    VerifyLastRequestStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAnonim = table.Column<bool>(type: "bit", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    BlockedUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceToken",
                table: "Devices",
                column: "DeviceToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
