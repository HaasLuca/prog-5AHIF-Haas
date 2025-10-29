using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashRegister.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GreetingText",
                table: "Greetings",
                newName: "Unit");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Greetings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Greetings",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Greetings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitQuantity",
                table: "Greetings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Greetings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Greetings");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Greetings");

            migrationBuilder.DropColumn(
                name: "UnitQuantity",
                table: "Greetings");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Greetings",
                newName: "GreetingText");
        }
    }
}
