using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurvayBucketsApi.Migrations
{
    /// <inheritdoc />
    public partial class asdqqewrfs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_AspNetUsers_userId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_polls_pollId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteAnswer_Answers_AnswerId",
                table: "VoteAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteAnswer_Questions_QuestionId",
                table: "VoteAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteAnswer_Vote_VoteId",
                table: "VoteAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteAnswer",
                table: "VoteAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vote",
                table: "Vote");

            migrationBuilder.RenameTable(
                name: "VoteAnswer",
                newName: "VoteAnswers");

            migrationBuilder.RenameTable(
                name: "Vote",
                newName: "Votes");

            migrationBuilder.RenameIndex(
                name: "IX_VoteAnswer_VoteId",
                table: "VoteAnswers",
                newName: "IX_VoteAnswers_VoteId");

            migrationBuilder.RenameIndex(
                name: "IX_VoteAnswer_QuestionId",
                table: "VoteAnswers",
                newName: "IX_VoteAnswers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_VoteAnswer_AnswerId",
                table: "VoteAnswers",
                newName: "IX_VoteAnswers_AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_userId_pollId",
                table: "Votes",
                newName: "IX_Votes_userId_pollId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_pollId",
                table: "Votes",
                newName: "IX_Votes_pollId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteAnswers",
                table: "VoteAnswers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteAnswers_Answers_AnswerId",
                table: "VoteAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoteAnswers_Questions_QuestionId",
                table: "VoteAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoteAnswers_Votes_VoteId",
                table: "VoteAnswers",
                column: "VoteId",
                principalTable: "Votes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteAnswers_Answers_AnswerId",
                table: "VoteAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteAnswers_Questions_QuestionId",
                table: "VoteAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_VoteAnswers_Votes_VoteId",
                table: "VoteAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_userId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_polls_pollId",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteAnswers",
                table: "VoteAnswers");

            migrationBuilder.RenameTable(
                name: "Votes",
                newName: "Vote");

            migrationBuilder.RenameTable(
                name: "VoteAnswers",
                newName: "VoteAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_userId_pollId",
                table: "Vote",
                newName: "IX_Vote_userId_pollId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_pollId",
                table: "Vote",
                newName: "IX_Vote_pollId");

            migrationBuilder.RenameIndex(
                name: "IX_VoteAnswers_VoteId",
                table: "VoteAnswer",
                newName: "IX_VoteAnswer_VoteId");

            migrationBuilder.RenameIndex(
                name: "IX_VoteAnswers_QuestionId",
                table: "VoteAnswer",
                newName: "IX_VoteAnswer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_VoteAnswers_AnswerId",
                table: "VoteAnswer",
                newName: "IX_VoteAnswer_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vote",
                table: "Vote",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteAnswer",
                table: "VoteAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_AspNetUsers_userId",
                table: "Vote",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_polls_pollId",
                table: "Vote",
                column: "pollId",
                principalTable: "polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoteAnswer_Answers_AnswerId",
                table: "VoteAnswer",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoteAnswer_Questions_QuestionId",
                table: "VoteAnswer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoteAnswer_Vote_VoteId",
                table: "VoteAnswer",
                column: "VoteId",
                principalTable: "Vote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
