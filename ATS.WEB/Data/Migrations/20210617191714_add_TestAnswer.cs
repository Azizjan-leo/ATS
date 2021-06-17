using Microsoft.EntityFrameworkCore.Migrations;

namespace ATS.WEB.Migrations
{
    public partial class add_TestAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_TestResults_TestResultId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_TestResultId",
                table: "Answers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74a13404-eccb-45ff-9060-f97e653c2c61");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a40d660-a54a-4202-ad0d-2f2c8a4e9bd4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a709017d-df15-45fa-a331-9e4b4e3defde");

            migrationBuilder.DropColumn(
                name: "TestResultId",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "TestAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAnswer = table.Column<bool>(type: "bit", nullable: false),
                    TestResultId = table.Column<int>(type: "int", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    AnswerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestAnswers_TestResults_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "TestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "312eb2fe-b6bb-49b4-a27c-744e7a602157", "21467acc-7752-49a5-85bb-4c7c7259ef47", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d151484a-435a-4396-97e8-cc5b2fce2a43", "dd67a25d-342b-4b30-ba23-b384de412a7b", "Teacher", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1473ca6d-14fe-4f1b-a2e3-2457e1099770", "86894fff-3fc1-4ab2-b359-44f207b66331", "Student", null });

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_AnswerId",
                table: "TestAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_QuestionId",
                table: "TestAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswers_TestResultId",
                table: "TestAnswers",
                column: "TestResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestAnswers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1473ca6d-14fe-4f1b-a2e3-2457e1099770");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "312eb2fe-b6bb-49b4-a27c-744e7a602157");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d151484a-435a-4396-97e8-cc5b2fce2a43");

            migrationBuilder.AddColumn<int>(
                name: "TestResultId",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a709017d-df15-45fa-a331-9e4b4e3defde", "171ee3e1-b89f-406b-abbd-7141c1ddcd58", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "74a13404-eccb-45ff-9060-f97e653c2c61", "12721f9f-37b0-46c0-80dc-9ffc33201c3b", "Teacher", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a40d660-a54a-4202-ad0d-2f2c8a4e9bd4", "ac222b2c-5d99-4368-b748-52d13d4b64cd", "Student", null });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TestResultId",
                table: "Answers",
                column: "TestResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_TestResults_TestResultId",
                table: "Answers",
                column: "TestResultId",
                principalTable: "TestResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
