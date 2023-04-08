﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatCalculator.UI.Shared.Api.LocalDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddDayDateBindEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayDateBinds",
                columns: table => new
                {
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

            migrationBuilder.CreateIndex(
                name: "IX_DayDateBinds_DayId",
                table: "DayDateBinds",
                column: "DayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayDateBinds");
        }
    }
}
