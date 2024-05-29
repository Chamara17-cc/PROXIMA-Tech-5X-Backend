using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class MyNewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileResources_Tasks_TaskId",
                table: "FileResources");

            migrationBuilder.DropColumn(
                name: "ProfilePictureLink",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "FileResources",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "TaskTimes",
                columns: table => new
                {
                    TaskTimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskTimeStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskTimeCompleteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalTimeTaskTimeDuration = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    DeveloperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTimes", x => x.TaskTimeId);
                    table.ForeignKey(
                        name: "FK_TaskTimes_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "DeveloperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTimes_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskTimes_DeveloperId",
                table: "TaskTimes",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTimes_TaskId",
                table: "TaskTimes",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileResources_Tasks_TaskId",
                table: "FileResources",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileResources_Tasks_TaskId",
                table: "FileResources");

            migrationBuilder.DropTable(
                name: "TaskTimes");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureLink",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "FileResources",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileResources_Tasks_TaskId",
                table: "FileResources",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
