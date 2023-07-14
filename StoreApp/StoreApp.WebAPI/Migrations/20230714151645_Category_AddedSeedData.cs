using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Category_AddedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "4d1b52e4-df0b-4232-8a6e-7789e84d0388");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "579aa2e2-e4ee-4ed5-b555-70f0241a86b9");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "a54e77a8-9e8f-494e-83cc-f04945a6e6c2");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Network" },
                    { 3, "Database Management Systems" }
                });

            //migrationBuilder.InsertData(
            //    table: "Roles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { "09fd4254-cd50-45f7-a8de-c1697997843b", "b47701ce-317c-4c8b-8652-96f2a2ca0071", "User", "USER" },
            //        { "18a0adfc-54bb-4920-b308-a41dddbe6a9a", "0ad453ff-e390-498b-b0c5-f48655cbef94", "Admin", "ADMIN" },
            //        { "6517d97f-f1f3-4ce6-9fb7-418fcc0a42bc", "7be4f364-4ed3-4b83-b0e7-f06a8cb528c5", "Editor", "EDITOR" }
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "09fd4254-cd50-45f7-a8de-c1697997843b");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "18a0adfc-54bb-4920-b308-a41dddbe6a9a");

            //migrationBuilder.DeleteData(
            //    table: "Roles",
            //    keyColumn: "Id",
            //    keyValue: "6517d97f-f1f3-4ce6-9fb7-418fcc0a42bc");

            //migrationBuilder.InsertData(
            //    table: "Roles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { "4d1b52e4-df0b-4232-8a6e-7789e84d0388", "eb8cfc78-4b55-4574-99bd-a6a141cb6a37", "Admin", "ADMIN" },
            //        { "579aa2e2-e4ee-4ed5-b555-70f0241a86b9", "96984ccd-d88b-419a-96d4-2af645cffdd6", "Editor", "EDITOR" },
            //        { "a54e77a8-9e8f-494e-83cc-f04945a6e6c2", "e55a37be-21d4-422a-8aae-45c9f480ab36", "User", "USER" }
            //    });
        }
    }
}
