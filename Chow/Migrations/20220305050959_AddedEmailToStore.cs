using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chow.Migrations
{
    public partial class AddedEmailToStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                columns: new[] { "Email", "Is_Open" },
                values: new object[] { "lekeboj@gmail.com", true });

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                column: "Email",
                value: "bolubee95@gmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Stores");

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                column: "Is_Open",
                value: false);
        }
    }
}
