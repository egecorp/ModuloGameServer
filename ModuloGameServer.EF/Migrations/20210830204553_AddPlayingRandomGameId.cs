using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddPlayingRandomGameId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayingRandomGameId",
                table: "Games",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayingRandomGameId",
                table: "Games");
        }
    }
}
