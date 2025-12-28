using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removenumberofrounds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRounds",
                table: "InterviewRounds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRounds",
                table: "InterviewRounds",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
