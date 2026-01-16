using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshop.Migrations
{
    /// <inheritdoc />
    public partial class Added_CartItemId_In_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartItemId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartItemId",
                table: "Orders",
                column: "CartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cart_CartItemId",
                table: "Orders",
                column: "CartItemId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cart_CartItemId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CartItemId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CartItemId",
                table: "Orders");
        }
    }
}
