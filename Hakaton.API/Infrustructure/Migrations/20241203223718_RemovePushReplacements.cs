using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hakaton.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovePushReplacements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushReplacementUsers_PushReplacements_pushReplacementId",
                table: "PushReplacementUsers");

            migrationBuilder.DropTable(
                name: "PushReplacements");

            migrationBuilder.RenameColumn(
                name: "pushReplacementId",
                table: "PushReplacementUsers",
                newName: "ReplacementId");

            migrationBuilder.RenameIndex(
                name: "IX_PushReplacementUsers_pushReplacementId",
                table: "PushReplacementUsers",
                newName: "IX_PushReplacementUsers_ReplacementId");

            migrationBuilder.AddForeignKey(
                name: "FK_PushReplacementUsers_Replacements_ReplacementId",
                table: "PushReplacementUsers",
                column: "ReplacementId",
                principalTable: "Replacements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PushReplacementUsers_Replacements_ReplacementId",
                table: "PushReplacementUsers");

            migrationBuilder.RenameColumn(
                name: "ReplacementId",
                table: "PushReplacementUsers",
                newName: "pushReplacementId");

            migrationBuilder.RenameIndex(
                name: "IX_PushReplacementUsers_ReplacementId",
                table: "PushReplacementUsers",
                newName: "IX_PushReplacementUsers_pushReplacementId");

            migrationBuilder.CreateTable(
                name: "PushReplacements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReplacementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushReplacements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PushReplacements_Replacements_ReplacementId",
                        column: x => x.ReplacementId,
                        principalTable: "Replacements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PushReplacements_ReplacementId",
                table: "PushReplacements",
                column: "ReplacementId");

            migrationBuilder.AddForeignKey(
                name: "FK_PushReplacementUsers_PushReplacements_pushReplacementId",
                table: "PushReplacementUsers",
                column: "pushReplacementId",
                principalTable: "PushReplacements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
