using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMarket.Migrations
{
    /// <inheritdoc />
    public partial class deleteLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Comment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
