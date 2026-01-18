using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurvayBucketsApi.Migrations
{
    /// <inheritdoc />
    public partial class addstest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "Admin@email.com", "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAIAAYagAAAAEGaGoarjWXzEJsT2ft6ZAJzOZTy1p0+jSJDvNOe9/A4Tsrg7DyWg6cxIwlcRDerIfg==", "Admin@email.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b",
                columns: new[] { "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "Admin@emaail.com", "ADMIN@EMAAIL.COM", "ADMIN@EMAAIL.COM", "AQAAAAIAAYagAAAAEDgKTA8H89DC7EgC0AHGiRLkR3WgwUlItT0fOORMW3X2Vd96PF95QMa50e/cYee35Q==", "Admin@emaail.com" });
        }
    }
}
