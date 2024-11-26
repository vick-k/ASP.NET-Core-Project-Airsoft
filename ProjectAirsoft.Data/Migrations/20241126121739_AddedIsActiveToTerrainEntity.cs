using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAirsoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActiveToTerrainEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Terrains",
                type: "bit",
                nullable: false,
                defaultValue: true,
                comment: "Flag for active/inactive terrain");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Terrains");
        }
    }
}
