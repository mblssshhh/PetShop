using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_ClubCards_BuyerId",
            //    table: "ClubCards");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ClubCards_BuyerId",
            //    table: "ClubCards",
            //    column: "BuyerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClubCards_BuyerId",
                table: "ClubCards");

            migrationBuilder.CreateIndex(
                name: "IX_ClubCards_BuyerId",
                table: "ClubCards",
                column: "BuyerId",
                unique: true);
        }
    }
}
