using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddBots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBot",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    BotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BotRequestToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServerToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NicName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    OwnerUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.BotId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bots_NicName",
                table: "Bots",
                column: "NicName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bots");

            migrationBuilder.DropColumn(
                name: "IsBot",
                table: "Users");
        }
    }
}
