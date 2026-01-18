using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurvayBucketsApi.Migrations
{
    /// <inheritdoc />
    public partial class addseddingtables1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e8c2fab5-2202-4ccb-81e6-460cb2ea9400", "3fceb675-2f7c-4d44-9441-bafa982656d8", false, false, "Admin", "ADMIN" },
                    { "f3e9f9a4-aaca-4766-b4a9-d568abc9c677", "c0d6f5da-2bca-4918-b8a0-527bd95216f0", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDgKTA8H89DC7EgC0AHGiRLkR3WgwUlItT0fOORMW3X2Vd96PF95QMa50e/cYee35Q==");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permessions", "poll:read", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 2, "Permessions", "poll:add", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 3, "Permessions", "poll:update", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 4, "Permessions", "poll:delete", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 5, "Permessions", "question:read", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 6, "Permessions", "question:add", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 7, "Permessions", "question:update", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 8, "Permessions", "user:read", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 9, "Permessions", "user:add", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 10, "Permessions", "user:update", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 11, "Permessions", "role:read", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 12, "Permessions", "role:add", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 13, "Permessions", "role:update", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" },
                    { 14, "Permessions", "result:read", "e8c2fab5-2202-4ccb-81e6-460cb2ea9400" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e8c2fab5-2202-4ccb-81e6-460cb2ea9400", "88f4a9e7-95df-4e45-8654-25155664279b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3e9f9a4-aaca-4766-b4a9-d568abc9c677");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e8c2fab5-2202-4ccb-81e6-460cb2ea9400", "88f4a9e7-95df-4e45-8654-25155664279b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAjMwDFHASxE+kwA5ab85U6WECfxsCyyogA9l7/oyPmu9YL9TUwymE9SSQ+svTBp2w==");
        }
    }
}
