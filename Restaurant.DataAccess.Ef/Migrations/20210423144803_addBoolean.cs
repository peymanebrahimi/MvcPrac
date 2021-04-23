using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.DataAccess.Ef.Migrations
{
    public partial class addBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CoffeeShop",
                table: "Restaurants",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoffeeShop",
                table: "Restaurants");
        }
    }
}
