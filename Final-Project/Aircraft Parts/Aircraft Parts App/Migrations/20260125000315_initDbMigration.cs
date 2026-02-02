using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aircraft_Parts_App.Migrations
{
    /// <inheritdoc />
    public partial class initDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Parts",
                columns: new[] { "Id", "Manufacturer", "NIIN", "PartName", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, "Boeing", "123456789", "Sonar", 12456 },
                    { 2, "Pratt & Whitney", "987654321", "Turbine Engine", 8420 },
                    { 3, "Lockheed Martin", "456789123", "Landing Gear Assembly", 3200 },
                    { 4, "Northrop Grumman", "789123456", "Avionics Control Unit", 15670 },
                    { 5, "Parker Aerospace", "321654987", "Hydraulic Pump", 9850 },
                    { 6, "Raytheon", "654987321", "Radar System", 5430 },
                    { 7, "General Electric", "147258369", "Fuel Injection Nozzle", 22100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parts");
        }
    }
}
