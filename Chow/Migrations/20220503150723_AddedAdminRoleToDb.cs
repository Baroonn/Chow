using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chow.Migrations
{
    public partial class AddedAdminRoleToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ad58eac-6c65-4c2b-b412-4934bac4ad81", "463fa9ec-a4b4-4462-aa29-98b8f17ce687", "Admin", "ADMIN" },
                    { "3b3b43fd-05c3-4d6e-aed6-cb51dc40ceb2", "c94b5320-db79-4ca5-8161-efaec399da70", "Rider", "RIDER" },
                    { "6dcbbca3-805e-4528-965d-7f6e7b04bf8b", "c6e768a4-4b07-4019-b6bd-83827e24f814", "Seller", "SELLER" },
                    { "b6c459a5-2ebf-486a-93d6-fbe2cb9813f1", "7bb57a64-cc3b-4fe7-a83e-5aceb53b7fb7", "Buyer", "BUYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ad58eac-6c65-4c2b-b412-4934bac4ad81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b3b43fd-05c3-4d6e-aed6-cb51dc40ceb2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dcbbca3-805e-4528-965d-7f6e7b04bf8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6c459a5-2ebf-486a-93d6-fbe2cb9813f1");

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
    }
}
