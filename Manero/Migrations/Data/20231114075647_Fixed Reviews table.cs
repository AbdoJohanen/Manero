using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manero.Migrations.Data
{
    /// <inheritdoc />
    public partial class FixedReviewstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Review",
                table: "Reviews",
                newName: "Reviewer");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "Reviewer",
                table: "Reviews",
                newName: "Review");
        }
    }
}
