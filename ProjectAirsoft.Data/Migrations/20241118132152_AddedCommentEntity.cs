using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ProjectAirsoft.Data.Migrations
{
	/// <inheritdoc />
	public partial class AddedCommentEntity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Comments",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false, comment: "The comment identifier")
						.Annotation("SqlServer:Identity", "1, 1"),
					Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The content of the comment"),
					CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date and time when the comment is created"),
					AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The author identifier"),
					GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The game identifier")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Comments", x => x.Id);
					table.ForeignKey(
						name: "FK_Comments_AspNetUsers_AuthorId",
						column: x => x.AuthorId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Comments_Games_GameId",
						column: x => x.GameId,
						principalTable: "Games",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Comments_AuthorId",
				table: "Comments",
				column: "AuthorId");

			migrationBuilder.CreateIndex(
				name: "IX_Comments_GameId",
				table: "Comments",
				column: "GameId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Comments");
		}
	}
}
