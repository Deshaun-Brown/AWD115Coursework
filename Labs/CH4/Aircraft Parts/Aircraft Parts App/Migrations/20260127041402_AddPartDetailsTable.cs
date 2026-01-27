using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aircraft_Parts_App.Migrations
{
    /// <inheritdoc />
    public partial class AddPartDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartDetails",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimensions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Certification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierContact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartDetails", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_PartDetails_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PartDetails",
                columns: new[] { "PartId", "Certification", "Condition", "Dimensions", "Material", "SupplierContact", "UnitPrice", "Weight" },
                values: new object[,]
                {
                    { 1, "FAA-PMA", "New", "120x80x60 cm", "Titanium Alloy", "boeing.parts@boeing.com", 12500.00m, "45.5 kg" },
                    { 2, "FAA-TSO", "New", "280x150x180 cm", "Nickel-based Superalloy", "service@pwc.com", 450000.00m, "2850 kg" },
                    { 3, "FAA-PMA", "Overhauled", "200x100x150 cm", "Steel Alloy", "parts@lockheedmartin.com", 85000.00m, "320 kg" },
                    { 4, "DO-160", "New", "45x35x20 cm", "Aluminum Composite", "avionics@northropgrumman.com", 28500.00m, "12.8 kg" },
                    { 5, "FAA-PMA", "New", "60x40x35 cm", "Stainless Steel", "sales@parker.com", 6750.00m, "18.5 kg" },
                    { 6, "MIL-STD-461", "New", "150x80x70 cm", "Composite Materials", "defense@raytheon.com", 175000.00m, "95 kg" },
                    { 7, "FAA-TSO", "New", "25x15x10 cm", "Ceramic Composite", "aviation@ge.com", 3200.00m, "2.3 kg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartDetails");
        }
    }
}
