using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectAirsoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae2d88fd-c3fc-4f53-b16f-bbb3388f3e77", "AQAAAAIAAYagAAAAEH2kMUnbd+D2Dw9VwVRxWdfSDUXcPDQ9LorkYOlel5I+M4DS82/HdoWN8FTmuls+iA==", "7379def1-f076-45c3-96df-bacc11cf569b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("184bf0c1-f0c6-41ba-94f4-fc8efcb6d0f9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f4983579-1168-48b7-8e4f-f242b0859ec0", "AQAAAAIAAYagAAAAECmSChtVxQEtgoPpIF8FFL7xKL1WEl0GUrKlnN62GKpUXnkC2BtsnhOH8k+x34REaA==", "b9242c54-dccc-4eca-a61f-3c2cb44f4e20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2c05c9e-643e-4fd0-9ab4-b01fa657c6b2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "100717ab-a483-4200-a76a-18eb0672d7cc", "AQAAAAIAAYagAAAAED8KOp0Whl+0LjZ98zE3Dd6DvLAwrGjHAjsPcPiqOhTnM9g4bIL5HkvJENFKFUCo1Q==", "8ab1ada7-ac57-4094-9a4f-8ec1619a692c" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CityId", "IsDeleted", "LeaderId", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("6ec23209-e40e-49a2-8ea4-052e83a2fe4d"), 3, false, new Guid("184bf0c1-f0c6-41ba-94f4-fc8efcb6d0f9"), "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRhpEkZQsmay7ehKLLMajwMXwhdo_hGLtBR6A&s", "Drazki Spec. Ops. Group" },
                    { new Guid("c379084f-85f5-43c9-a448-17c5514059d6"), 10, false, new Guid("d2c05c9e-643e-4fd0-9ab4-b01fa657c6b2"), "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDoQ68zwAWyAjM0wwucWB7FbRO_l0EUmMwLg&s", "OPFOR TCD" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("6ec23209-e40e-49a2-8ea4-052e83a2fe4d"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("c379084f-85f5-43c9-a448-17c5514059d6"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fc17956-b5b2-490f-bd14-b31de1289650", "AQAAAAIAAYagAAAAEA8N2sKcVQUCBr+bFvwv5HKHfb5ZCk7JhYbm6nta02rxk6iE997Ugbh3GYX755HdKA==", "20dfb824-3278-495e-a401-43fb8bdf61f3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("184bf0c1-f0c6-41ba-94f4-fc8efcb6d0f9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2828fdd-980f-41d0-bac2-2f8a30538ce6", "AQAAAAIAAYagAAAAEBZnpUEZ4czhZ1l08HcDlzfyoYPQFrFzOkkRNH8VGfko1FCMv8+/fowLEPEXir9Gvg==", "094377f0-813c-49b8-9dff-159f05206053" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2c05c9e-643e-4fd0-9ab4-b01fa657c6b2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4204148f-0754-4012-ab45-ab17bd7403aa", "AQAAAAIAAYagAAAAEI8GMjBqjbJLjQa4k7pcPhYC80H1i19qsg4SFgSkPITHQyEDa2ftdQyJjuuY6vgjaQ==", "fb7bce0c-e5a7-4ecb-8e4e-800d542498ca" });
        }
    }
}
