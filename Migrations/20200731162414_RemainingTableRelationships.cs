using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodTruckRodeo.API.Migrations
{
  public partial class RemainingTableRelationships : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "Email",
          table: "Users",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Name",
          table: "Users",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "PhoneNumber",
          table: "Users",
          nullable: true);

      migrationBuilder.CreateTable(
          name: "FoodTrucks",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(nullable: true),
            Tagline = table.Column<string>(nullable: true),
            Description = table.Column<string>(nullable: true),
            Phone = table.Column<string>(nullable: true),
            Email = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_FoodTrucks", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "CalendarEvents",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(nullable: true),
            Date = table.Column<DateTime>(nullable: false),
            StartTime = table.Column<DateTime>(nullable: false),
            EndTime = table.Column<DateTime>(nullable: false),
            Location = table.Column<string>(nullable: true),
            Address = table.Column<string>(nullable: true),
            Address2 = table.Column<string>(nullable: true),
            City = table.Column<string>(nullable: true),
            State = table.Column<string>(nullable: true),
            ZipCode = table.Column<string>(nullable: true),
            FoodTruckId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_CalendarEvents", x => x.Id);
            table.ForeignKey(
                      name: "FK_CalendarEvents_FoodTrucks_FoodTruckId",
                      column: x => x.FoodTruckId,
                      principalTable: "FoodTrucks",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "ContactRequests",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(nullable: true),
            Email = table.Column<string>(nullable: true),
            Message = table.Column<string>(nullable: true),
            FoodTruckId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_ContactRequests", x => x.Id);
            table.ForeignKey(
                      name: "FK_ContactRequests_FoodTrucks_FoodTruckId",
                      column: x => x.FoodTruckId,
                      principalTable: "FoodTrucks",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "FoodTruckUsers",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            IsAdmin = table.Column<bool>(nullable: false),
            FoodTruckId = table.Column<int>(nullable: false),
            UserId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_FoodTruckUsers", x => x.Id);
            table.ForeignKey(
                      name: "FK_FoodTruckUsers_FoodTrucks_FoodTruckId",
                      column: x => x.FoodTruckId,
                      principalTable: "FoodTrucks",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_FoodTruckUsers_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Menus",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(nullable: true),
            SortOrder = table.Column<int>(nullable: true),
            IsActive = table.Column<bool>(nullable: false),
            FoodTruckId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Menus", x => x.Id);
            table.ForeignKey(
                      name: "FK_Menus_FoodTrucks_FoodTruckId",
                      column: x => x.FoodTruckId,
                      principalTable: "FoodTrucks",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Carts",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            SubTotal = table.Column<float>(nullable: false),
            Tax = table.Column<float>(nullable: false),
            Total = table.Column<float>(nullable: false),
            IsPurchaseComplete = table.Column<bool>(nullable: false),
            IsOrderFilled = table.Column<bool>(nullable: false),
            FoodTruckUserId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Carts", x => x.Id);
            table.ForeignKey(
                      name: "FK_Carts_FoodTruckUsers_FoodTruckUserId",
                      column: x => x.FoodTruckUserId,
                      principalTable: "FoodTruckUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Items",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(nullable: true),
            Description = table.Column<string>(nullable: true),
            Price = table.Column<float>(nullable: false),
            Size = table.Column<string>(nullable: true),
            IsSoldOut = table.Column<bool>(nullable: false),
            MenuId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Items", x => x.Id);
            table.ForeignKey(
                      name: "FK_Items_Menus_MenuId",
                      column: x => x.MenuId,
                      principalTable: "Menus",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "CartItemDetails",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Quantity = table.Column<int>(nullable: false),
            ItemId = table.Column<int>(nullable: false),
            CartId = table.Column<int>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_CartItemDetails", x => x.Id);
            table.ForeignKey(
                      name: "FK_CartItemDetails_Carts_CartId",
                      column: x => x.CartId,
                      principalTable: "Carts",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
          });

      migrationBuilder.CreateIndex(
          name: "IX_CalendarEvents_FoodTruckId",
          table: "CalendarEvents",
          column: "FoodTruckId");

      migrationBuilder.CreateIndex(
          name: "IX_CartItemDetails_CartId",
          table: "CartItemDetails",
          column: "CartId");

      migrationBuilder.CreateIndex(
          name: "IX_Carts_FoodTruckUserId",
          table: "Carts",
          column: "FoodTruckUserId");

      migrationBuilder.CreateIndex(
          name: "IX_ContactRequests_FoodTruckId",
          table: "ContactRequests",
          column: "FoodTruckId");

      migrationBuilder.CreateIndex(
          name: "IX_FoodTruckUsers_FoodTruckId",
          table: "FoodTruckUsers",
          column: "FoodTruckId");

      migrationBuilder.CreateIndex(
          name: "IX_FoodTruckUsers_UserId",
          table: "FoodTruckUsers",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_Items_MenuId",
          table: "Items",
          column: "MenuId");

      migrationBuilder.CreateIndex(
          name: "IX_Menus_FoodTruckId",
          table: "Menus",
          column: "FoodTruckId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "CalendarEvents");

      migrationBuilder.DropTable(
          name: "CartItemDetails");

      migrationBuilder.DropTable(
          name: "ContactRequests");

      migrationBuilder.DropTable(
          name: "Items");

      migrationBuilder.DropTable(
          name: "Carts");

      migrationBuilder.DropTable(
          name: "Menus");

      migrationBuilder.DropTable(
          name: "FoodTruckUsers");

      migrationBuilder.DropTable(
          name: "FoodTrucks");

      migrationBuilder.DropColumn(
          name: "Email",
          table: "Users");

      migrationBuilder.DropColumn(
          name: "Name",
          table: "Users");

      migrationBuilder.DropColumn(
          name: "PhoneNumber",
          table: "Users");
    }
  }
}
