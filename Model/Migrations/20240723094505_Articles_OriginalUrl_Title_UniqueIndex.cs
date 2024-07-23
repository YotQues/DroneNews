using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DroneNews.Model.Migrations
{
    /// <inheritdoc />
    public partial class Articles_OriginalUrl_Title_UniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_Title",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_OriginalUrl",
                table: "Articles",
                column: "OriginalUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Title",
                table: "Articles",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_OriginalUrl",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_Title",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Title",
                table: "Articles",
                column: "Title");
        }
    }
}
