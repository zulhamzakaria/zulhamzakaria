using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCourseSite.Migrations
{
    public partial class ModifiedCategoryItemEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CategroyItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CategroyItems");
        }
    }
}
