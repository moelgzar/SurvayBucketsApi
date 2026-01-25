using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurvayBucketsApi.Migrations
{
    /// <inheritdoc />
    public partial class test123456 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDgKTA8H89DC7EgC0AHGiRLkR3WgwUlItT0fOORMW3X2Vd96PF95QMa50e/cYee35Q==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "88f4a9e7-95df-4e45-8654-25155664279b",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELlExqeY3u92oZS25c+6QXADw4H0VFn2YbyguN2PzvYTTp2m0M2MvDSmvPbhEl2uhw==");
        }
    }
}
