using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddGameAndGameRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_DynamicUserInfo_DynamicUserInfoUserId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_DynamicUserInfo_DynamicUserInfoUserId1",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_Game_DynamicUserInfoUserId1",
                table: "Games",
                newName: "IX_Games_DynamicUserInfoUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Game_DynamicUserInfoUserId",
                table: "Games",
                newName: "IX_Games_DynamicUserInfoUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GameRound",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    RoundNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Digit1 = table.Column<int>(type: "int", nullable: false),
                    Digit2 = table.Column<int>(type: "int", nullable: false),
                    Digit3 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRound_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameRound_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_User1Id",
                table: "Games",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_User2Id",
                table: "Games",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_GameRound_GameId",
                table: "GameRound",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRound_UserId",
                table: "GameRound",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_User1Id",
                table: "Games",
                column: "User1Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_User2Id",
                table: "Games",
                column: "User2Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_DynamicUserInfo_DynamicUserInfoUserId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_DynamicUserInfo_DynamicUserInfoUserId1",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_User1Id",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_User2Id",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GameRound");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_User1Id",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_User2Id",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameIndex(
                name: "IX_Games_DynamicUserInfoUserId1",
                table: "Game",
                newName: "IX_Game_DynamicUserInfoUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Games_DynamicUserInfoUserId",
                table: "Game",
                newName: "IX_Game_DynamicUserInfoUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_DynamicUserInfo_DynamicUserInfoUserId",
                table: "Game",
                column: "DynamicUserInfoUserId",
                principalTable: "DynamicUserInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_DynamicUserInfo_DynamicUserInfoUserId1",
                table: "Game",
                column: "DynamicUserInfoUserId1",
                principalTable: "DynamicUserInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
