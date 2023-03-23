using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatCalculator.UI.Shared.Api.LocalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddMealCountForDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarbohydrateMealCount",
                table: "Days",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FatMealCount",
                table: "Days",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProteinMealCount",
                table: "Days",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbohydrateMealCount",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "FatMealCount",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "ProteinMealCount",
                table: "Days");
        }
    }
}
