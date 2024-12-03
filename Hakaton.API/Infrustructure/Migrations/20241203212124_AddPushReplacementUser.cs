using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hakaton.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPushReplacementUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "PushReplacementUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    pushReplacementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushReplacementUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PushReplacementUsers_PushReplacements_pushReplacementId",
                        column: x => x.pushReplacementId,
                        principalTable: "PushReplacements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PushReplacementUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PushReplacements_ReplacementId",
                table: "PushReplacements",
                column: "ReplacementId");

            migrationBuilder.CreateIndex(
                name: "IX_PushReplacementUsers_pushReplacementId",
                table: "PushReplacementUsers",
                column: "pushReplacementId");

            migrationBuilder.CreateIndex(
                name: "IX_PushReplacementUsers_UserId",
                table: "PushReplacementUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PushReplacementUsers");

            migrationBuilder.DropTable(
                name: "PushReplacements");
        }
    }
}
