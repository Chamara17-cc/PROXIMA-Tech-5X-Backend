﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class totalhours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonthlyWorkedHours",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyWorkedHours",
                table: "Payments");
        }
    }
}
