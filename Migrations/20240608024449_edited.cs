using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMarket.Migrations
{
    /// <inheritdoc />
    public partial class edited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartContent_Carts_CartId",
                table: "CartContent");

            migrationBuilder.DropForeignKey(
                name: "FK_CartContent_Products_ProductId",
                table: "CartContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartContent",
                table: "CartContent");

            migrationBuilder.RenameTable(
                name: "CartContent",
                newName: "CartContents");

            migrationBuilder.RenameIndex(
                name: "IX_CartContent_ProductId",
                table: "CartContents",
                newName: "IX_CartContents_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartContent_CartId",
                table: "CartContents",
                newName: "IX_CartContents_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartContents",
                table: "CartContents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartContents_Carts_CartId",
                table: "CartContents",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartContents_Products_ProductId",
                table: "CartContents",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartContents_Carts_CartId",
                table: "CartContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CartContents_Products_ProductId",
                table: "CartContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartContents",
                table: "CartContents");

            migrationBuilder.RenameTable(
                name: "CartContents",
                newName: "CartContent");

            migrationBuilder.RenameIndex(
                name: "IX_CartContents_ProductId",
                table: "CartContent",
                newName: "IX_CartContent_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartContents_CartId",
                table: "CartContent",
                newName: "IX_CartContent_CartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartContent",
                table: "CartContent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartContent_Carts_CartId",
                table: "CartContent",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartContent_Products_ProductId",
                table: "CartContent",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
