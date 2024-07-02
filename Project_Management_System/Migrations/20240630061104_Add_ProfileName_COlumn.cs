using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Add_ProfileName_COlumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageName",
                table: "Users");
        }
    }
}
