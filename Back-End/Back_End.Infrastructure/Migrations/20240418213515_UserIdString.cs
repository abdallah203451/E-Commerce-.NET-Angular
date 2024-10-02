using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Back_End.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserIdString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId1",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId1",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b495bc3-b340-4756-a0a4-116f1f9c5075");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb829b94-e1b9-4866-9dd5-50f1f720cdbd");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7b5566ce-5dce-432a-93dc-48c0c76e4d71", null, "User", "USER" },
                    { "ed96ed57-3c19-438d-a978-1d03196e38e7", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b5566ce-5dce-432a-93dc-48c0c76e4d71");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed96ed57-3c19-438d-a978-1d03196e38e7");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Carts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8b495bc3-b340-4756-a0a4-116f1f9c5075", null, "User", "USER" },
                    { "eb829b94-e1b9-4866-9dd5-50f1f720cdbd", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId1",
                table: "Carts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId1",
                table: "Carts",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
