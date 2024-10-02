using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Back_End.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_UserId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ec546a1-8e33-4549-8543-1b0277462416");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6e6d96c-fe11-4d80-bca2-75abbada2119");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UserId",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "006a668b-8a2a-4ef1-a191-a35813c8c0a7", null, "User", "USER" },
                    { "7f9a38cc-385c-4cf5-851f-fe73d4838f03", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "006a668b-8a2a-4ef1-a191-a35813c8c0a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f9a38cc-385c-4cf5-851f-fe73d4838f03");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Product",
                newName: "IX_Product_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ec546a1-8e33-4549-8543-1b0277462416", null, "Administrator", "ADMINISTRATOR" },
                    { "c6e6d96c-fe11-4d80-bca2-75abbada2119", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_UserId",
                table: "Product",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
