using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAirsoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStartTimeToGameEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "Games",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                comment: "The time when the game will start");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Games");
        }
    }
}
