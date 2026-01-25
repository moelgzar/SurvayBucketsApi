using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurvayBucketsApi.Migrations
{
    /// <inheritdoc />
    public partial class addguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "RoleId",
                value: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019bf204-d0d9-7518-9022-d59ad4c9f580", "019bf204-d0d9-73bc-ae0b-aa3e4a671670", true, false, "Member", "MEMBER" },
                    { "019bf204-d0d9-7b88-b10d-7226cef7dbb3", "019bf204-d0d9-7fb6-a0d7-6c3872f6d28b", false, false, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019bf204-d0d9-71f0-bb5c-a29a7a7d1dab", 0, "019bf204-d0d9-7afb-9389-e14682c0f65b", "Admin@email.com", true, "Survey Basket", false, "Admin", false, null, "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAEDgKTA8H89DC7EgC0AHGiRLkR3WgwUlItT0fOORMW3X2Vd96PF95QMa50e/cYee35Q==", null, false, "efc11bfa1c1e4100a8e7ece811066671", false, "Admin@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "019bf204-d0d9-7b88-b10d-7226cef7dbb3", "019bf204-d0d9-71f0-bb5c-a29a7a7d1dab" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019bf204-d0d9-7518-9022-d59ad4c9f580");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019bf204-d0d9-7b88-b10d-7226cef7dbb3", "019bf204-d0d9-71f0-bb5c-a29a7a7d1dab" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019bf204-d0d9-7b88-b10d-7226cef7dbb3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019bf204-d0d9-71f0-bb5c-a29a7a7d1dab");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "RoleId",
                value: "e8c2fab5-2202-4ccb-81e6-460cb2ea9400");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e8c2fab5-2202-4ccb-81e6-460cb2ea9400", "3fceb675-2f7c-4d44-9441-bafa982656d8", false, false, "Admin", "ADMIN" },
                    { "f3e9f9a4-aaca-4766-b4a9-d568abc9c677", "c0d6f5da-2bca-4918-b8a0-527bd95216f0", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "88f4a9e7-95df-4e45-8654-25155664279b", 0, "4efe56a5-713c-4a9e-b2c4-f17efdb28081", "Admin@email.com", true, "Survey Basket", false, "Admin", false, null, "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAEDgKTA8H89DC7EgC0AHGiRLkR3WgwUlItT0fOORMW3X2Vd96PF95QMa50e/cYee35Q==", null, false, "efc11bfa1c1e4100a8e7ece811066671", false, "Admin@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e8c2fab5-2202-4ccb-81e6-460cb2ea9400", "88f4a9e7-95df-4e45-8654-25155664279b" });
        }
    }
}
