using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddIsCancelToUserConfirmation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                table: "UserConfirmations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancel",
                table: "UserConfirmations");
        }
    }
}
