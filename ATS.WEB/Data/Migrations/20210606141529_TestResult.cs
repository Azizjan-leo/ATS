using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ATS.WEB.Migrations
{
    public partial class TestResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestResultId",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswererId = table.Column<int>(type: "int", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: true),
                    PassDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_Lessons_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResults_Students_AnswererId",
                        column: x => x.AnswererId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResults_Teachers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestResultId",
                table: "Answers",
                column: "TestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_AnswererId_TopicId",
                table: "TestResults",
                columns: new[] { "AnswererId", "TopicId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_ReviewerId",
                table: "TestResults",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TopicId",
                table: "TestResults",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_TestResults_TestResultId",
                table: "Answers",
                column: "TestResultId",
                principalTable: "TestResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_TestResults_TestResultId",
                table: "Answers");

            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropIndex(
                name: "IX_Answers_TestResultId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "TestResultId",
                table: "Answers");
        }
    }
}
