using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectAirsoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TeamId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), 0, "0fc17956-b5b2-490f-bd14-b31de1289650", "manager@gmail.com", false, false, true, null, "MANAGER@GMAIL.COM", "MANAGER", "AQAAAAIAAYagAAAAEA8N2sKcVQUCBr+bFvwv5HKHfb5ZCk7JhYbm6nta02rxk6iE997Ugbh3GYX755HdKA==", null, false, "20dfb824-3278-495e-a401-43fb8bdf61f3", null, false, "manager" },
                    { new Guid("184bf0c1-f0c6-41ba-94f4-fc8efcb6d0f9"), 0, "e2828fdd-980f-41d0-bac2-2f8a30538ce6", "player1@gmail.com", false, false, true, null, "PLAYER1@GMAIL.COM", "PLAYER1", "AQAAAAIAAYagAAAAEBZnpUEZ4czhZ1l08HcDlzfyoYPQFrFzOkkRNH8VGfko1FCMv8+/fowLEPEXir9Gvg==", null, false, "094377f0-813c-49b8-9dff-159f05206053", null, false, "player1" },
                    { new Guid("d2c05c9e-643e-4fd0-9ab4-b01fa657c6b2"), 0, "4204148f-0754-4012-ab45-ab17bd7403aa", "player2@gmail.com", false, false, true, null, "PLAYER2@GMAIL.COM", "PLAYER2", "AQAAAAIAAYagAAAAEI8GMjBqjbJLjQa4k7pcPhYC80H1i19qsg4SFgSkPITHQyEDa2ftdQyJjuuY6vgjaQ==", null, false, "fb7bce0c-e5a7-4ecb-8e4e-800d542498ca", null, false, "player2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("184bf0c1-f0c6-41ba-94f4-fc8efcb6d0f9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2c05c9e-643e-4fd0-9ab4-b01fa657c6b2"));
        }
    }
}
