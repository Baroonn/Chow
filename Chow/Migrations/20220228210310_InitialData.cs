using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chow.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealComponents_Stores_Store_Id",
                table: "MealComponents");

            migrationBuilder.RenameColumn(
                name: "Store_Id",
                table: "MealComponents",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_MealComponents_Store_Id",
                table: "MealComponents",
                newName: "IX_MealComponents_StoreId");

            migrationBuilder.InsertData(
                table: "MealComponents",
                columns: new[] { "MealComponentId", "Is_Available", "Name", "Price", "StoreId", "Type" },
                values: new object[,]
                {
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), false, "Beef", 500, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "topping" },
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), false, "Ofada Rice", 1500, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "main" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), false, "Fish Stew", 0, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "soup" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreId", "Is_Open", "Location", "Name", "Phone" },
                values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), false, "Iwo Road", "Nana's Kitchen", "+2349134946613" });

            migrationBuilder.AddForeignKey(
                name: "FK_MealComponents_Stores_StoreId",
                table: "MealComponents",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealComponents_Stores_StoreId",
                table: "MealComponents");

            migrationBuilder.DeleteData(
                table: "MealComponents",
                keyColumn: "MealComponentId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "MealComponents",
                keyColumn: "MealComponentId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "MealComponents",
                keyColumn: "MealComponentId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "MealComponents",
                newName: "Store_Id");

            migrationBuilder.RenameIndex(
                name: "IX_MealComponents_StoreId",
                table: "MealComponents",
                newName: "IX_MealComponents_Store_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealComponents_Stores_Store_Id",
                table: "MealComponents",
                column: "Store_Id",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
