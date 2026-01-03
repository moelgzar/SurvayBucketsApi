using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurvayBucketsApi.Migrations
{
    /// <inheritdoc />
    public partial class vote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_userId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_polls_pollId",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Votes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "pollId",
                table: "Votes",
                newName: "PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_userId_pollId",
                table: "Votes",
                newName: "IX_Votes_UserId_PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_pollId",
                table: "Votes",
                newName: "IX_Votes_PollId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_polls_PollId",
                table: "Votes",
                column: "PollId",
                principalTable: "polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_polls_PollId",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Votes",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "PollId",
                table: "Votes",
                newName: "pollId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_UserId_PollId",
                table: "Votes",
                newName: "IX_Votes_userId_pollId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_PollId",
                table: "Votes",
                newName: "IX_Votes_pollId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_userId",
                table: "Votes",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_polls_pollId",
                table: "Votes",
                column: "pollId",
                principalTable: "polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
