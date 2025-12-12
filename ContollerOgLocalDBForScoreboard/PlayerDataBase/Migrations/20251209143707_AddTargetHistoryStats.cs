using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerDataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetHistoryStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerTargetHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerScoreboardId = table.Column<int>(type: "int", nullable: false),
                    TargetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesShot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTargetHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerTargetHistories_PlayerScoreboards_PlayerScoreboardId",
                        column: x => x.PlayerScoreboardId,
                        principalTable: "PlayerScoreboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTargetHistories_PlayerScoreboardId",
                table: "PlayerTargetHistories",
                column: "PlayerScoreboardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerTargetHistories");
        }
    }
}
