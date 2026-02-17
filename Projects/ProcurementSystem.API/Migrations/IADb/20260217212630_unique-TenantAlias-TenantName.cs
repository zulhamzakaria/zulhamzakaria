using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcurementSystem.API.Migrations.IADb
{
    /// <inheritdoc />
    public partial class uniqueTenantAliasTenantName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tenants_TenantAlias",
                table: "Tenants",
                column: "TenantAlias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_TenantName",
                table: "Tenants",
                column: "TenantName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_TenantAlias",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_TenantName",
                table: "Tenants");
        }
    }
}
