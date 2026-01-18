using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurvayBucketsApi.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "88f4a9e7-95df-4e45-8654-25155664279b", 0, "4efe56a5-713c-4a9e-b2c4-f17efdb28081", "Admin@emaail.com", true, "Survey Basket", "Admin", false, null, "ADMIN@EMAAIL.COM", "ADMIN@EMAAIL.COM", "AQAAAAIAAYagAAAAEAjMwDFHASxE+kwA5ab85U6WECfxsCyyogA9l7/oyPmu9YL9TUwymE9SSQ+svTBp2w==", null, false, "efc11bfa1c1e4100a8e7ece811066671", false, "Admin@emaail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b");
        }
    }
}
