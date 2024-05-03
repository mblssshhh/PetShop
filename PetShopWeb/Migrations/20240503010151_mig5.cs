using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_Baskets_BasketId",
            //    table: "Orders");

            //migrationBuilder.DropTable(
            //    name: "Baskets");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Weight",
            //    table: "Products",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(double),
            //    oldType: "float");

            migrationBuilder.AddColumn<decimal>(
                name: "Money",
                table: "Buyers",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            //migrationBuilder.CreateTable(
            //    name: "Buskets",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BuyerId = table.Column<int>(type: "int", nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        Count = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Buskets", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Buskets_Buyers_BuyerId",
            //            column: x => x.BuyerId,
            //            principalTable: "Buyers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Buskets_Products_ProductId",
            //            column: x => x.ProductId,
            //            principalTable: "Products",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Buskets_BuyerId",
            //    table: "Buskets",
            //    column: "BuyerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Buskets_ProductId",
            //    table: "Buskets",
            //    column: "ProductId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_Buskets_BasketId",
            //    table: "Orders",
            //    column: "BasketId",
            //    principalTable: "Buskets",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Buskets_BasketId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Buskets");

            migrationBuilder.DropColumn(
                name: "Money",
                table: "Buyers");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Baskets_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_BuyerId",
                table: "Baskets",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_ProductId",
                table: "Baskets",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Baskets_BasketId",
                table: "Orders",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
