using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodTruckRodeo.API.Migrations
{
    public partial class AddURLToCalendarEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemDetails_Carts_CartId",
                table: "CartItemDetails");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartItemDetails",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressUrlEscaped",
                table: "CalendarEvents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapUrl",
                table: "CalendarEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItemDetails_ItemId",
                table: "CartItemDetails",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemDetails_Carts_CartId",
                table: "CartItemDetails",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemDetails_Items_ItemId",
                table: "CartItemDetails",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItemDetails_Carts_CartId",
                table: "CartItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItemDetails_Items_ItemId",
                table: "CartItemDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartItemDetails_ItemId",
                table: "CartItemDetails");

            migrationBuilder.DropColumn(
                name: "AddressUrlEscaped",
                table: "CalendarEvents");

            migrationBuilder.DropColumn(
                name: "MapUrl",
                table: "CalendarEvents");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "CartItemDetails",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CartItemDetails_Carts_CartId",
                table: "CartItemDetails",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
