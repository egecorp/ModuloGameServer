using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class DynamicUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GUID",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "DynamicUserInfo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicUserInfo", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_DynamicUserInfo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinutesPerRound = table.Column<int>(type: "int", nullable: false),
                    User1Id = table.Column<int>(type: "int", nullable: true),
                    User2Id = table.Column<int>(type: "int", nullable: true),
                    IsStart = table.Column<bool>(type: "bit", nullable: false),
                    IsFinish = table.Column<bool>(type: "bit", nullable: false),
                    IsTimeout = table.Column<bool>(type: "bit", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false),
                    DynamicUserInfoUserId = table.Column<int>(type: "int", nullable: true),
                    DynamicUserInfoUserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_DynamicUserInfo_DynamicUserInfoUserId",
                        column: x => x.DynamicUserInfoUserId,
                        principalTable: "DynamicUserInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_DynamicUserInfo_DynamicUserInfoUserId1",
                        column: x => x.DynamicUserInfoUserId1,
                        principalTable: "DynamicUserInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_DynamicUserInfoUserId",
                table: "Game",
                column: "DynamicUserInfoUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_DynamicUserInfoUserId1",
                table: "Game",
                column: "DynamicUserInfoUserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "DynamicUserInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "GUID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
