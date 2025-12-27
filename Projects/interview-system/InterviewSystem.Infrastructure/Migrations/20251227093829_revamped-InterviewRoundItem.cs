using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class revampedInterviewRoundItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "InterviewRoundItems",
                newName: "AllowedPosition");

            migrationBuilder.AddColumn<bool>(
                name: "AllowMultiple",
                table: "InterviewRoundItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanCompleteProcess",
                table: "InterviewRoundItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMandatory",
                table: "InterviewRoundItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowMultiple",
                table: "InterviewRoundItems");

            migrationBuilder.DropColumn(
                name: "CanCompleteProcess",
                table: "InterviewRoundItems");

            migrationBuilder.DropColumn(
                name: "IsMandatory",
                table: "InterviewRoundItems");

            migrationBuilder.RenameColumn(
                name: "AllowedPosition",
                table: "InterviewRoundItems",
                newName: "Position");
        }
    }
}
