using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuloGameServer.EF.Migrations
{
    public partial class AddDigits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "D1_1_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1_1_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1_1_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1_2_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1_2_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D1_2_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2_1_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2_1_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2_1_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2_2_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2_2_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D2_2_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3_1_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3_1_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3_1_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3_2_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3_2_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D3_2_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4_1_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4_1_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4_1_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4_2_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4_2_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D4_2_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D5_1_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D5_1_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D5_1_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D5_2_1",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D5_2_2",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "D5_2_3",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "D1_1_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D1_1_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D1_1_3",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D1_2_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D1_2_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D1_2_3",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D2_1_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D2_1_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D2_1_3",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D2_2_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D2_2_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D2_2_3",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D3_1_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D3_1_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D3_1_3",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D3_2_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D3_2_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D3_2_3",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D4_1_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D4_1_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D4_1_3",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D4_2_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D4_2_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D4_2_3",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D5_1_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D5_1_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D5_1_3",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D5_2_1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D5_2_2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "D5_2_3",
                table: "Games");
        }
    }
}
