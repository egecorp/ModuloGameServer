using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddCharacterAndEmotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Character",
                table: "DynamicUserInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Emotion",
                table: "DynamicUserInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Character",
                table: "DynamicUserInfo");

            migrationBuilder.DropColumn(
                name: "Emotion",
                table: "DynamicUserInfo");
        }
    }
}
