using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerDataBase.Migrations
{
    /// <inheritdoc />
    public partial class InitPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerScoreboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalBountyCollected = table.Column<int>(type: "int", nullable: false),
                    TargetsTakenOut = table.Column<int>(type: "int", nullable: false),
                    HighestBountyCollected = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerScoreboards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerScoreboards");
        }
    }
}
