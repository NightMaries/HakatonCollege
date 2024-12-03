using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hakaton.API.Migrations
{
    /// <inheritdoc />
    public partial class updateSchuduleAndStudyWeek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start_Date",
                table: "StudyWeeks",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End_Date",
                table: "StudyWeeks",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "PairStart",
                table: "Schedules",
                newName: "ScheduleStart");

            migrationBuilder.RenameColumn(
                name: "PairNumber",
                table: "Schedules",
                newName: "ScheduleNumber");

            migrationBuilder.RenameColumn(
                name: "PairEnd",
                table: "Schedules",
                newName: "ScheduleEnd");

            migrationBuilder.AddColumn<int>(
                name: "StudyWeekNumber",
                table: "StudyWeeks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyWeekNumber",
                table: "StudyWeeks");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "StudyWeeks",
                newName: "Start_Date");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "StudyWeeks",
                newName: "End_Date");

            migrationBuilder.RenameColumn(
                name: "ScheduleStart",
                table: "Schedules",
                newName: "PairStart");

            migrationBuilder.RenameColumn(
                name: "ScheduleNumber",
                table: "Schedules",
                newName: "PairNumber");

            migrationBuilder.RenameColumn(
                name: "ScheduleEnd",
                table: "Schedules",
                newName: "PairEnd");
        }
    }
}
