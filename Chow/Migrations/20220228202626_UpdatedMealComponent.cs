using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chow.Migrations
{
    public partial class UpdatedMealComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "MealComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "MealComponents");
        }
    }
}
