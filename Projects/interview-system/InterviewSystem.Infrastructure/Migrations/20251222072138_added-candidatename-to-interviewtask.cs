using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedcandidatenametointerviewtask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CandidateName",
                table: "InterviewTasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CandidateName",
                table: "InterviewTasks");
        }
    }
}
