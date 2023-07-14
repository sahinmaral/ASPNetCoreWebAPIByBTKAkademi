using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Role_AddedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40655cd1-54b0-46be-b9b6-895c0e41f366", "aca4edd4-cc68-4ac6-abfe-64a04a441c87", "Editor", "EDITOR" },
                    { "d18d55ed-ec04-4be1-abf8-e961a1133051", "aef65d01-2d69-4b5a-aa92-9abcba220df1", "User", "USER" },
                    { "da49a58c-6a6d-4dc9-b4bb-b1b2cca08eba", "a36b3c20-1269-4ea8-be22-5de81d8ce586", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "40655cd1-54b0-46be-b9b6-895c0e41f366");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d18d55ed-ec04-4be1-abf8-e961a1133051");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "da49a58c-6a6d-4dc9-b4bb-b1b2cca08eba");
        }
    }
}
