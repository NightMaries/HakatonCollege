using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hakaton.API.Migrations
{
    /// <inheritdoc />
    public partial class UserAddSubscritions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Subscription",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subscription",
                table: "Users");
        }
    }
}
