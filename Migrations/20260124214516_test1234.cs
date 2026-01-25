using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurvayBucketsApi.Migrations
{
    /// <inheritdoc />
    public partial class test1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFNJtQn66RnJSwKj6R466zlUIJtaiqem/SAYIXRtTZ+4svRL00WLpVTomDHb4EkZJA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDglRUxq8XrzSwgYxb0JDLkyITJ4PopH5bQgVDkmmNcm2zPXjs1DstIolHGAAq7nSQ==");
        }
    }
}
