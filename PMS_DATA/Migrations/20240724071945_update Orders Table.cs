using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS_DATA.Migrations
{
    /// <inheritdoc />
    public partial class updateOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "OrderQty",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderQty",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "TotalAmount");
        }
    }
}
