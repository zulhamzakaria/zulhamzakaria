using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class linkedinterviewtasktointerviewprocess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentSequence",
                table: "InterviewProcesses",
                newName: "CurrentRoundSequence");

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                table: "InterviewTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AssigneeName",
                table: "InterviewTasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InterviewProcessId",
                table: "InterviewTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "RoundSequence",
                table: "InterviewTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "InterviewTasks");

            migrationBuilder.DropColumn(
                name: "AssigneeName",
                table: "InterviewTasks");

            migrationBuilder.DropColumn(
                name: "InterviewProcessId",
                table: "InterviewTasks");

            migrationBuilder.DropColumn(
                name: "RoundSequence",
                table: "InterviewTasks");

            migrationBuilder.RenameColumn(
                name: "CurrentRoundSequence",
                table: "InterviewProcesses",
                newName: "CurrentSequence");
        }
    }
}
