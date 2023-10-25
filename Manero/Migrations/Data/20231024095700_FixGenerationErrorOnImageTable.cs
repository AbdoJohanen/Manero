using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manero.Migrations.Data
{
    /// <inheritdoc />
    public partial class FixGenerationErrorOnImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductEntityArticleNumber",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductEntityArticleNumber",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ArticleNumber",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ProductEntityArticleNumber",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ProductArticleNumber",
                table: "Images",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductArticleNumber",
                table: "Images",
                column: "ProductArticleNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductArticleNumber",
                table: "Images",
                column: "ProductArticleNumber",
                principalTable: "Products",
                principalColumn: "ArticleNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductArticleNumber",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductArticleNumber",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ProductArticleNumber",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ArticleNumber",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductEntityArticleNumber",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductEntityArticleNumber",
                table: "Images",
                column: "ProductEntityArticleNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductEntityArticleNumber",
                table: "Images",
                column: "ProductEntityArticleNumber",
                principalTable: "Products",
                principalColumn: "ArticleNumber");
        }
    }
}
