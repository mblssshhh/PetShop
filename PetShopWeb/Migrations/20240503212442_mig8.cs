using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_Buskets_BasketId",
            //    table: "Orders");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_Staffs_StaffId",
            //    table: "Orders");

            //migrationBuilder.RenameColumn(
            //    name: "StaffId",
            //    table: "Orders",
            //    newName: "IdStaff");

            //migrationBuilder.RenameColumn(
            //    name: "BasketId",
            //    table: "Orders",
            //    newName: "BusketId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Orders_StaffId",
            //    table: "Orders",
            //    newName: "IX_Orders_IdStaff");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Orders_BasketId",
            //    table: "Orders",
            //    newName: "IX_Orders_BusketId");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Buskets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_Buskets_BusketId",
            //    table: "Orders",
            //    column: "BusketId",
            //    principalTable: "Buskets",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_Staffs_IdStaff",
            //    table: "Orders",
            //    column: "IdStaff",
            //    principalTable: "Staffs",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Buskets_BusketId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Staffs_IdStaff",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Buskets");

            migrationBuilder.RenameColumn(
                name: "IdStaff",
                table: "Orders",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "BusketId",
                table: "Orders",
                newName: "BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_IdStaff",
                table: "Orders",
                newName: "IX_Orders_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_BusketId",
                table: "Orders",
                newName: "IX_Orders_BasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Buskets_BasketId",
                table: "Orders",
                column: "BasketId",
                principalTable: "Buskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Staffs_StaffId",
                table: "Orders",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
