using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAirsoft.Data.Migrations
{
	/// <inheritdoc />
	public partial class RenamedUserGamesToUsersGames : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_UserGames_AspNetUsers_UserId",
				table: "UserGames");

			migrationBuilder.DropForeignKey(
				name: "FK_UserGames_Games_GameId",
				table: "UserGames");

			migrationBuilder.DropPrimaryKey(
				name: "PK_UserGames",
				table: "UserGames");

			migrationBuilder.RenameTable(
				name: "UserGames",
				newName: "UsersGames");

			migrationBuilder.RenameIndex(
				name: "IX_UserGames_GameId",
				table: "UsersGames",
				newName: "IX_UsersGames_GameId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_UsersGames",
				table: "UsersGames",
				columns: new[] { "UserId", "GameId" });

			migrationBuilder.AddForeignKey(
				name: "FK_UsersGames_AspNetUsers_UserId",
				table: "UsersGames",
				column: "UserId",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);

			migrationBuilder.AddForeignKey(
				name: "FK_UsersGames_Games_GameId",
				table: "UsersGames",
				column: "GameId",
				principalTable: "Games",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_UsersGames_AspNetUsers_UserId",
				table: "UsersGames");

			migrationBuilder.DropForeignKey(
				name: "FK_UsersGames_Games_GameId",
				table: "UsersGames");

			migrationBuilder.DropPrimaryKey(
				name: "PK_UsersGames",
				table: "UsersGames");

			migrationBuilder.RenameTable(
				name: "UsersGames",
				newName: "UserGames");

			migrationBuilder.RenameIndex(
				name: "IX_UsersGames_GameId",
				table: "UserGames",
				newName: "IX_UserGames_GameId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_UserGames",
				table: "UserGames",
				columns: new[] { "UserId", "GameId" });

			migrationBuilder.AddForeignKey(
				name: "FK_UserGames_AspNetUsers_UserId",
				table: "UserGames",
				column: "UserId",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_UserGames_Games_GameId",
				table: "UserGames",
				column: "GameId",
				principalTable: "Games",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
