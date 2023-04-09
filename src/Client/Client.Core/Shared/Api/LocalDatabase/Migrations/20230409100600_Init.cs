using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Client.Core.Shared.Api.LocalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ProteinPercentages = table.Column<double>(type: "REAL", nullable: false),
                    FatPercentages = table.Column<double>(type: "REAL", nullable: false),
                    CarbohydratePercentages = table.Column<double>(type: "REAL", nullable: false),
                    ProteinMealCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FatMealCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CarbohydrateMealCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Grams = table.Column<double>(type: "REAL", nullable: false),
                    Protein = table.Column<double>(type: "REAL", nullable: false),
                    Fat = table.Column<double>(type: "REAL", nullable: false),
                    Carbohydrate = table.Column<double>(type: "REAL", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayDateBinds",
                columns: table => new {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayDateBinds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayDateBinds_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Portions",
                columns: table => new {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MealId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProteinPercentages = table.Column<double>(type: "REAL", nullable: false),
                    FatPercentages = table.Column<double>(type: "REAL", nullable: false),
                    CarbohydratePercentages = table.Column<double>(type: "REAL", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portions_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Portions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Carbohydrate", "Description", "Fat", "Grams", "Order", "Protein", "Title" },
                values: new object[,]
                {
                    { 1, 20.600000000000001, null, 1.1000000000000001, 100.0, 1, 4.2000000000000002, "Гречка" },
                    { 2, 24.690000000000001, null, 0.5, 100.0, 2, 2.2000000000000002, "Рис" },
                    { 3, 0.59999999999999998, null, 8.1999999999999993, 100.0, 3, 21.0, "Курица" },
                    { 4, 0.0, null, 7.2999999999999998, 100.0, 4, 26.800000000000001, "Индейка" },
                    { 5, 3.0, null, 5.0, 100.0, 5, 16.0, "Творог" },
                    { 6, 5.7000000000000002, null, 6.2000000000000002, 100.0, 6, 80.299999999999997, "Протеин" },
                    { 7, 0.0, null, 0.90000000000000002, 100.0, 7, 15.9, "Минтай" },
                    { 8, 61.799999999999997, null, 6.2000000000000002, 100.0, 8, 12.300000000000001, "Овсянка" },
                    { 9, 0.0, null, 99.799999999999997, 100.0, 9, 0.0, "Масло льняное" },
                    { 10, 0.0, null, 99.799999999999997, 100.0, 10, 0.0, "Масло оливковое" },
                    { 11, 0.69999999999999996, null, 11.5, 100.0, 11, 12.699999999999999, "Яйцо" },
                    { 12, 21.399999999999999, null, 0.69999999999999996, 100.0, 12, 3.6000000000000001, "Макароны из твёрдых сортов" },
                    { 13, 68.0, null, 1.5, 100.0, 13, 13.0, "Булгур" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayDateBinds_DayId",
                table: "DayDateBinds",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DayId",
                table: "Meals",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Portions_MealId",
                table: "Portions",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Portions_ProductId",
                table: "Portions",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayDateBinds");

            migrationBuilder.DropTable(
                name: "Portions");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Days");
        }
    }
}
