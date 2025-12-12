using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileLib.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationToCurrentUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "CurrentUsers",
                type: "float",
                nullable: true,
                defaultValue: 55.630767667077436);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "CurrentUsers",
                type: "float",
                nullable: true,
                defaultValue: 12.078199489762422);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "CurrentUsers");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "CurrentUsers");
        }
    }
}
