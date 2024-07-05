using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class c : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InitialBudeget",
                table: "Projects",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialBudeget",
                table: "Projects");
        }
    }
}
