using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentPlatformAPI.Migrations
{
    public partial class changeDateNamesInCalendarEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "CalendarEvent",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "CalendarEvent",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "CalendarEvent",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "CalendarEvent",
                newName: "End");
        }
    }
}
