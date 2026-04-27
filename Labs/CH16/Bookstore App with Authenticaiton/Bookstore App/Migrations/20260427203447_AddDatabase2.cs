using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore_App.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 2, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 23, 8 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 24, 9 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 25, 10 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 26, 11 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 27, 12 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 28, 13 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 29, 14 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 30, 15 });
        }
    }
}
