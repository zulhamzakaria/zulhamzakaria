using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedInterviewDatetoInterviewTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "InterviewDate",
                table: "InterviewTasks",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterviewDate",
                table: "InterviewTasks");
        }
    }
}
