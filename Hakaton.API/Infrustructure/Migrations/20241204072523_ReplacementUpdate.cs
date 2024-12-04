using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hakaton.API.Migrations
{
    /// <inheritdoc />
    public partial class ReplacementUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewTeacherId",
                table: "Replacements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OldTeacherId",
                table: "Replacements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ScheduleForTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    StudyWeeks = table.Column<int[]>(type: "integer[]", nullable: false),
                    WeekDay = table.Column<string>(type: "text", nullable: false),
                    PairNumber = table.Column<int>(type: "integer", nullable: false),
                    StudyWeekId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleForTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleForTeachers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleForTeachers_StudyWeeks_StudyWeekId",
                        column: x => x.StudyWeekId,
                        principalTable: "StudyWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleForTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Replacements_NewTeacherId",
                table: "Replacements",
                column: "NewTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Replacements_OldTeacherId",
                table: "Replacements",
                column: "OldTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleForTeachers_GroupId",
                table: "ScheduleForTeachers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleForTeachers_StudyWeekId",
                table: "ScheduleForTeachers",
                column: "StudyWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleForTeachers_TeacherId",
                table: "ScheduleForTeachers",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replacements_Teachers_NewTeacherId",
                table: "Replacements",
                column: "NewTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replacements_Teachers_OldTeacherId",
                table: "Replacements",
                column: "OldTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replacements_Teachers_NewTeacherId",
                table: "Replacements");

            migrationBuilder.DropForeignKey(
                name: "FK_Replacements_Teachers_OldTeacherId",
                table: "Replacements");

            migrationBuilder.DropTable(
                name: "ScheduleForTeachers");

            migrationBuilder.DropIndex(
                name: "IX_Replacements_NewTeacherId",
                table: "Replacements");

            migrationBuilder.DropIndex(
                name: "IX_Replacements_OldTeacherId",
                table: "Replacements");

            migrationBuilder.DropColumn(
                name: "NewTeacherId",
                table: "Replacements");

            migrationBuilder.DropColumn(
                name: "OldTeacherId",
                table: "Replacements");
        }
    }
}
