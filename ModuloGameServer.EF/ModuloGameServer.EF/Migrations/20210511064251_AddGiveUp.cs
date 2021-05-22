using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddGiveUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_DynamicUserInfo_DynamicUserInfoUserId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_DynamicUserInfo_DynamicUserInfoUserId1",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DynamicUserInfoUserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DynamicUserInfoUserId1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DynamicUserInfoUserId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DynamicUserInfoUserId1",
                table: "Games");

            migrationBuilder.AddColumn<bool>(
                name: "IsGiveUp",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UserGiveUp",
                table: "GameRound",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGiveUp",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "UserGiveUp",
                table: "GameRound");

            migrationBuilder.AddColumn<int>(
                name: "DynamicUserInfoUserId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DynamicUserInfoUserId1",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_DynamicUserInfoUserId",
                table: "Games",
                column: "DynamicUserInfoUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DynamicUserInfoUserId1",
                table: "Games",
                column: "DynamicUserInfoUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DynamicUserInfo_DynamicUserInfoUserId",
                table: "Games",
                column: "DynamicUserInfoUserId",
                principalTable: "DynamicUserInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_DynamicUserInfo_DynamicUserInfoUserId1",
                table: "Games",
                column: "DynamicUserInfoUserId1",
                principalTable: "DynamicUserInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
