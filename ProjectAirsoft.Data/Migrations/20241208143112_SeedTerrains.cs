using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectAirsoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedTerrains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Terrains",
                columns: new[] { "Id", "CityId", "IsActive", "IsDeleted", "LocationUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("01ac2f70-21e0-417f-b18a-41c9aa1055b6"), 10, true, false, "https://maps.app.goo.gl/Uyhob7UD8pahXxXy5", "Green Water" },
                    { new Guid("63b8246a-aa5c-4964-95c9-10d42eb07b65"), 3, true, false, "https://maps.app.goo.gl/5ZFey2vRCUqduQZ48", "Antifreeze" },
                    { new Guid("838d3b35-4413-4721-870e-32b00bde5f8e"), 10, true, false, "https://maps.app.goo.gl/epw8PCoW2TsQamtF7", "Residence" },
                    { new Guid("8b031c23-6687-4338-9845-5e9af3af951c"), 3, true, false, "https://maps.app.goo.gl/f3MciMAqqMgjKzNd9", "Horse Base" },
                    { new Guid("a8177ba1-f5af-4ec4-8631-41a1a5250460"), 1, true, false, "https://maps.app.goo.gl/Q8E4aBXsvyYjyzRj6", "Airsoft Sofia Field" },
                    { new Guid("c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"), 1, true, false, "https://maps.app.goo.gl/qzZ24mimwva19fff7", "BoinoPole" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("01ac2f70-21e0-417f-b18a-41c9aa1055b6"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("63b8246a-aa5c-4964-95c9-10d42eb07b65"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("838d3b35-4413-4721-870e-32b00bde5f8e"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("8b031c23-6687-4338-9845-5e9af3af951c"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("a8177ba1-f5af-4ec4-8631-41a1a5250460"));

            migrationBuilder.DeleteData(
                table: "Terrains",
                keyColumn: "Id",
                keyValue: new Guid("c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"));
        }
    }
}
