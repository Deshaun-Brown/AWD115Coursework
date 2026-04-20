using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookstore_App.Migrations
{
    /// <inheritdoc />
    public partial class AddBoooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "GenreId", "Isbn", "Price", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 4, 1, 1, "9780385537030", 14.99m, new DateOnly(2011, 9, 13), "The Night Circus" },
                    { 5, 2, 2, "9780593135204", 18.99m, new DateOnly(2021, 5, 4), "Project Hail Mary" },
                    { 6, 3, 3, "9781250301697", 13.99m, new DateOnly(2019, 5, 1), "The Silent Patient" },
                    { 7, 1, 1, "9780393592389", 15.99m, new DateOnly(2018, 2, 20), "Educated" },
                    { 8, 2, 2, "9780316341370", 12.99m, new DateOnly(2018, 4, 10), "Circe" },
                    { 9, 3, 3, "9780553418026", 11.99m, new DateOnly(2014, 2, 11), "The Martian" },
                    { 10, 1, 1, "9780071767903", 12.49m, new DateOnly(2011, 8, 30), "The Song of Achilles" },
                    { 11, 2, 2, "9780735211292", 16.99m, new DateOnly(2018, 10, 16), "Atomic Habits" },
                    { 12, 3, 3, "9780735219090", 10.99m, new DateOnly(2018, 8, 14), "Where the Crawdads Sing" },
                    { 13, 1, 1, "9780525559474", 14.49m, new DateOnly(2020, 8, 13), "The Midnight Library" },
                    { 14, 2, 2, "9780143034902", 13.49m, new DateOnly(2016, 9, 6), "A Gentleman in Moscow" },
                    { 15, 3, 3, "9780525479988", 15.49m, new DateOnly(2020, 6, 2), "The Vanishing Half" },
                    { 16, 1, 1, "9780743273565", 10.99m, new DateOnly(1925, 4, 10), "The Great Gatsby" },
                    { 17, 2, 2, "9780061120084", 7.99m, new DateOnly(1960, 7, 11), "To Kill a Mockingbird" },
                    { 18, 3, 3, "9781485550464", 9.99m, new DateOnly(1813, 1, 28), "Pride and Prejudice" },
                    { 19, 1, 1, "9780316769488", 10.99m, new DateOnly(1951, 7, 16), "The Catcher in the Rye" },
                    { 20, 2, 2, "9780439708180", 10.99m, new DateOnly(1997, 9, 1), "Harry Potter and the Sorcerer's Stone" },
                    { 21, 3, 3, "9780547928227", 10.99m, new DateOnly(1937, 9, 21), "The Hobbit" },
                    { 22, 1, 1, "9780307474278", 10.99m, new DateOnly(2003, 3, 18), "The Da Vinci Code" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);
        }
    }
}
