using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerDataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddExtendedStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MostShotTarget",
                table: "PlayerScoreboards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalGames",
                table: "PlayerScoreboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalShots",
                table: "PlayerScoreboards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MostShotTarget",
                table: "PlayerScoreboards");

            migrationBuilder.DropColumn(
                name: "TotalGames",
                table: "PlayerScoreboards");

            migrationBuilder.DropColumn(
                name: "TotalShots",
                table: "PlayerScoreboards");
        }
    }
}
