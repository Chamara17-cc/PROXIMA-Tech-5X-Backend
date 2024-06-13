using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class missingpartsc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_DeveloperId",
                table: "Payments");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Budgets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DeveloperId",
                table: "Payments",
                column: "DeveloperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_DeveloperId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Budgets");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DeveloperId",
                table: "Payments",
                column: "DeveloperId",
                unique: true);
        }
    }
}
