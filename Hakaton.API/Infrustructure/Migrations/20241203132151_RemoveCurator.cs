using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hakaton.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCurator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Curators_CuratorId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "Curators");

            migrationBuilder.RenameColumn(
                name: "CuratorId",
                table: "Groups",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CuratorId",
                table: "Groups",
                newName: "IX_Groups_TeacherId");

            migrationBuilder.AlterColumn<string>(
                name: "WeekDay",
                table: "Schedules",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "PairEnd",
                table: "Schedules",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "PairNumber",
                table: "Schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "PairStart",
                table: "Schedules",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Replacements",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "PairNumber",
                table: "Replacements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Teachers_TeacherId",
                table: "Groups",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Teachers_TeacherId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "PairEnd",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "PairNumber",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "PairStart",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "PairNumber",
                table: "Replacements");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Groups",
                newName: "CuratorId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups",
                newName: "IX_Groups_CuratorId");

            migrationBuilder.AlterColumn<string>(
                name: "WeekDay",
                table: "Schedules",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "Replacements",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Curators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeacherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curators_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curators_TeacherId",
                table: "Curators",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Curators_CuratorId",
                table: "Groups",
                column: "CuratorId",
                principalTable: "Curators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
