using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMarket.Migrations
{
    /// <inheritdoc />
    public partial class editClasses2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewerName",
                table: "Ratings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewerName",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
