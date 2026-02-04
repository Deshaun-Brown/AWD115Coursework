using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fitness_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Non-slip yoga mat for fitness exercises", "/images/yoga-mat.jpg", "Yoga Mat", 29.99m, 50 },
                    { 2, "Adjustable dumbbells 5-50 lbs", "/images/dumbbells.jpg", "Dumbbells Set", 199.99m, 25 },
                    { 3, "Set of 5 resistance bands with different levels", "/images/resistance-bands.jpg", "Resistance Bands", 24.99m, 100 },
                    { 4, "Whey protein powder chocolate flavor 2lb", "/images/protein.jpg", "Protein Powder", 39.99m, 75 },
                    { 5, "Insulated stainless steel water bottle 32oz", "/images/water-bottle.jpg", "Water Bottle", 19.99m, 150 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
