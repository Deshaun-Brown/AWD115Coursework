using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore_App.Migrations
{
    /// <inheritdoc />
    public partial class AddFKFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "AuthorId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                column: "AuthorId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                column: "AuthorId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 4, 4 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 5, 5 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 6, 6 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 7, 7 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 8, 8 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 9, 9 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 10, 10 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 11, 11 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 12, 12 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 13, 13 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 14, 14 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 15, 15 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "AuthorId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                column: "AuthorId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                column: "AuthorId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 19, 4 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 20, 5 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 21, 6 });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "AuthorId", "GenreId" },
                values: new object[] { 22, 7 });
        }
    }
}
