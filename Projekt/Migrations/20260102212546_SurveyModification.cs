using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class SurveyModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionItems_Questions_QuestionId",
                table: "QuestionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionItems",
                table: "QuestionItems");

            migrationBuilder.RenameTable(
                name: "QuestionItems",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionItems_QuestionId",
                table: "Answers",
                newName: "IX_Answers_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "QuestionItems");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_QuestionId",
                table: "QuestionItems",
                newName: "IX_QuestionItems_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionItems",
                table: "QuestionItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionItems_Questions_QuestionId",
                table: "QuestionItems",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
