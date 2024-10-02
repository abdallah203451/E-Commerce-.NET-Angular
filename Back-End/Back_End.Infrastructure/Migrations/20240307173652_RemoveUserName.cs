using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Back_End.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ebf7c80-37fb-4445-af9a-316c7f219311");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "835164ba-af93-45c7-9278-260fed978541");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c19ef4dd-471b-49bf-aa0d-914cc4077da8", null, "Administrator", "ADMINISTRATOR" },
                    { "fba8080d-aeeb-48a4-9c8c-8f5ac97606a0", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c19ef4dd-471b-49bf-aa0d-914cc4077da8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fba8080d-aeeb-48a4-9c8c-8f5ac97606a0");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ebf7c80-37fb-4445-af9a-316c7f219311", null, "Administrator", "ADMINISTRATOR" },
                    { "835164ba-af93-45c7-9278-260fed978541", null, "User", "USER" }
                });
        }
    }
}
