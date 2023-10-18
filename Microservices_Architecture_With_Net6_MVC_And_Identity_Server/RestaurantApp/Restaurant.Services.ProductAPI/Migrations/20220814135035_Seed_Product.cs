using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Services.ProductAPI.Migrations
{
    public partial class Seed_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageURL", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Beef", "Burger with cheese", "https://cdn-image.foodandwine.com/sites/default/files/200306-r-xl-classic-beef-burgers.jpg", "Cheeseburger", 14.0 },
                    { 2, "Beef", "Burger with peanut butter", "https://cdn-image.foodandwine.com/sites/default/files/200306-r-xl-classic-beef-burgers.jpg", "Butterheaven", 17.0 },
                    { 3, "Chicken", "Glazed with Korean sauce", "https://simply-delicious-food.com/wp-content/uploads/2018/11/spicy-chicken-burgers-3.jpg", "Korean Fried Chicken", 16.0 },
                    { 4, "Chicken", "Chicken burger", "https://simply-delicious-food.com/wp-content/uploads/2018/11/spicy-chicken-burgers-3.jpg", "Bok Bok", 15.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);
        }
    }
}
