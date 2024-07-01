using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMarket.Migrations
{
    /// <inheritdoc />
    public partial class AdminMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminMessage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminMessage",
                table: "Products");
        }
    }
}
