using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chow.Migrations
{
    public partial class InitialStoreData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreId", "Is_Open", "Location", "Name", "Phone" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true, "Samonda", "Hexagon Rice", "+2347069570633" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
