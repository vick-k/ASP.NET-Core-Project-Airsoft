using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAirsoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsCanceledToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Flag for canceled games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Games");
        }
    }
}
