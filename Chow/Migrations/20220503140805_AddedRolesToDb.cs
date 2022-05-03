using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chow.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a1cb196-4fca-4af2-9d65-1f65d046b835", "82faf186-a3be-4c73-aee5-6e5ed61a3f6d", "Buyer", "BUYER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9457318b-6c77-4274-ae6a-922a96910da3", "52af7cc8-c32f-4dfd-863c-bbfa4206c780", "Seller", "SELLER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b5169f31-068e-48c9-8ded-6ae1059742b1", "f0952e2d-a06f-495c-aba4-1f10211de82d", "Rider", "RIDER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a1cb196-4fca-4af2-9d65-1f65d046b835");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9457318b-6c77-4274-ae6a-922a96910da3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5169f31-068e-48c9-8ded-6ae1059742b1");
        }
    }
}
