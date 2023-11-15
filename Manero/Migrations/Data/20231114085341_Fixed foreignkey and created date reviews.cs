using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manero.Migrations.Data
{
    /// <inheritdoc />
    public partial class Fixedforeignkeyandcreateddatereviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductArticleNumber",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProductArticleNumber",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProductArticleNumber",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleNumber",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ArticleNumber",
                table: "Reviews",
                column: "ArticleNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ArticleNumber",
                table: "Reviews",
                column: "ArticleNumber",
                principalTable: "Products",
                principalColumn: "ArticleNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ArticleNumber",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ArticleNumber",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleNumber",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProductArticleNumber",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductArticleNumber",
                table: "Reviews",
                column: "ProductArticleNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductArticleNumber",
                table: "Reviews",
                column: "ProductArticleNumber",
                principalTable: "Products",
                principalColumn: "ArticleNumber");
        }
    }
}
