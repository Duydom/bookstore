using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class DMdoi2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quantities_Carts_CartId",
                table: "Quantities");

            migrationBuilder.DropForeignKey(
                name: "FK_Quantities_Orders_OrderId",
                table: "Quantities");

            migrationBuilder.DropIndex(
                name: "IX_Quantities_CartId",
                table: "Quantities");

            migrationBuilder.DropIndex(
                name: "IX_Quantities_OrderId",
                table: "Quantities");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Quantities");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Quantities");

            migrationBuilder.CreateTable(
                name: "CartQuantity",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "int", nullable: false),
                    QuantitiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartQuantity", x => new { x.CartsId, x.QuantitiesId });
                    table.ForeignKey(
                        name: "FK_CartQuantity_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartQuantity_Quantities_QuantitiesId",
                        column: x => x.QuantitiesId,
                        principalTable: "Quantities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderQuantity",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    QuantitiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderQuantity", x => new { x.OrdersId, x.QuantitiesId });
                    table.ForeignKey(
                        name: "FK_OrderQuantity_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderQuantity_Quantities_QuantitiesId",
                        column: x => x.QuantitiesId,
                        principalTable: "Quantities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartQuantity_QuantitiesId",
                table: "CartQuantity",
                column: "QuantitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderQuantity_QuantitiesId",
                table: "OrderQuantity",
                column: "QuantitiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartQuantity");

            migrationBuilder.DropTable(
                name: "OrderQuantity");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Quantities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Quantities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quantities_CartId",
                table: "Quantities",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Quantities_OrderId",
                table: "Quantities",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantities_Carts_CartId",
                table: "Quantities",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quantities_Orders_OrderId",
                table: "Quantities",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
