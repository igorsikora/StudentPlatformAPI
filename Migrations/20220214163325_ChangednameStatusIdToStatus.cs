using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentPlatformAPI.Migrations
{
    public partial class ChangednameStatusIdToStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Task",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Task",
                newName: "StatusId");
        }
    }
}
