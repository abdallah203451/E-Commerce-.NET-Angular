using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Back_End.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserIdInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "380fc8c1-9d81-4070-a821-12f8c0f0eba7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73be4b76-424f-43f4-9870-1cefc3b1c7e6");

            migrationBuilder.DropColumn(
                name: "UserIdAddedBy",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70b33441-5cc1-4caa-b479-b20c76dee9c8", null, "Administrator", "ADMINISTRATOR" },
                    { "c41747e3-0174-4d40-8f6f-15fbf7bb89bf", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70b33441-5cc1-4caa-b479-b20c76dee9c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c41747e3-0174-4d40-8f6f-15fbf7bb89bf");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserIdAddedBy",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "380fc8c1-9d81-4070-a821-12f8c0f0eba7", null, "User", "USER" },
                    { "73be4b76-424f-43f4-9870-1cefc3b1c7e6", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
