using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcurementSystem.API.Migrations.IADb
{
    /// <inheritdoc />
    public partial class addedTenantStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantStatus",
                table: "Tenants",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantStatus",
                table: "Tenants");
        }
    }
}
