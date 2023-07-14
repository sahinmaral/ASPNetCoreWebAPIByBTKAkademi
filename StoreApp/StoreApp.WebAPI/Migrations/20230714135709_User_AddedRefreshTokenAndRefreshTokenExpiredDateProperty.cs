using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class User_AddedRefreshTokenAndRefreshTokenExpiredDateProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiredDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f5d1cbd-dfd6-4442-a5c0-10aec38420d4", "935b90b5-deef-4e0f-9c56-38658d067ec5", "Admin", "ADMIN" },
                    { "85fda8a3-283a-4168-8dd5-4bcd10bb1d8d", "ad25de65-9013-4082-9290-de888f8f350f", "Editor", "EDITOR" },
                    { "e3c22978-c9d0-4e4a-a5e5-fee9ddf41697", "8e11d46a-f297-425c-9aa3-c8f23cd487fe", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1f5d1cbd-dfd6-4442-a5c0-10aec38420d4");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "85fda8a3-283a-4168-8dd5-4bcd10bb1d8d");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e3c22978-c9d0-4e4a-a5e5-fee9ddf41697");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiredDate",
                table: "Users");

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
    }
}
