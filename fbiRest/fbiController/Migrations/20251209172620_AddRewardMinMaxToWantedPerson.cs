using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fbiController.Migrations
{
    /// <inheritdoc />
    public partial class AddRewardMinMaxToWantedPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WantedPeople",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "RewardMax",
                table: "WantedPeople",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RewardMin",
                table: "WantedPeople",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RewardMax",
                table: "WantedPeople");

            migrationBuilder.DropColumn(
                name: "RewardMin",
                table: "WantedPeople");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WantedPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
