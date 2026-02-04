using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aircraft_Parts_App.Migrations
{
    /// <inheritdoc />
    public partial class AllowPartsToBeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PartCategories",
                keyColumn: "PartCategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Avionics", "Electronic systems and components" },
                    { 2, "Propulsion", "Engines and related systems" },
                    { 3, "Structural", "Airframe and structural components" },
                    { 4, "Hydraulics", "Hydraulic systems and components" },
                    { 5, "Fuel Systems", "Fuel delivery and storage" }
                });

            migrationBuilder.InsertData(
                table: "PartCategories",
                columns: new[] { "PartCategoryId", "CategoryId", "PartId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 5, 2 },
                    { 4, 3, 3 },
                    { 5, 4, 3 },
                    { 6, 1, 4 },
                    { 7, 4, 5 },
                    { 8, 1, 6 },
                    { 9, 5, 7 },
                    { 10, 2, 7 }
                });
        }
    }
}
