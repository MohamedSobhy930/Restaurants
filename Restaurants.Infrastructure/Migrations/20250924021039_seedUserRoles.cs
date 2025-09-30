using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a6132b21-186c-4357-b353-731300e2cac9", null, "Owner", "OWNER" },
                    { "c7b013f0-5201-4317-abd8-c211f91b7330", null, "Admin", "ADMIN" },
                    { "e2e3e595-188e-4f40-8f6a-4b0c776a3b6e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6132b21-186c-4357-b353-731300e2cac9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2e3e595-188e-4f40-8f6a-4b0c776a3b6e");
        }
    }
}
