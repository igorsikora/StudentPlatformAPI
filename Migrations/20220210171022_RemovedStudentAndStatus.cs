using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentPlatformAPI.Migrations
{
    public partial class RemovedStudentAndStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvent_Student_StudentId",
                table: "CalendarEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Student_StudentId",
                table: "Task");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Task_StudentId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_CalendarEvent_StudentId",
                table: "CalendarEvent");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "CalendarEvent");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Task",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CalendarEvent",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Task_UserId",
                table: "Task",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvent_UserId",
                table: "CalendarEvent",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvent_AspNetUsers_UserId",
                table: "CalendarEvent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_UserId",
                table: "Task",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarEvent_AspNetUsers_UserId",
                table: "CalendarEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_UserId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_UserId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_CalendarEvent_UserId",
                table: "CalendarEvent");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CalendarEvent");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "CalendarEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 0, "ToDo" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "InProgress" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Done" });

            migrationBuilder.CreateIndex(
                name: "IX_Task_StudentId",
                table: "Task",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvent_StudentId",
                table: "CalendarEvent",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarEvent_Student_StudentId",
                table: "CalendarEvent",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Student_StudentId",
                table: "Task",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
