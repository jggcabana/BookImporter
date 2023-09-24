using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookImporter.Repositories.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ImportSchemaChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookImportFormat",
                table: "ImportLogs");

            migrationBuilder.AddColumn<string>(
                name: "BookImportFormat",
                table: "ImportBatches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookImportFormat",
                table: "ImportBatches");

            migrationBuilder.AddColumn<string>(
                name: "BookImportFormat",
                table: "ImportLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
