using Microsoft.EntityFrameworkCore.Migrations;

namespace ATS.WEB.Migrations
{
    public partial class Correct_Answer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "RightStudent",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TestResultQuestionId",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c289ef29-d7fc-4884-a694-2183fe9ab285", "390063fd-f315-492d-894d-018af3235821", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "21f4c81b-080b-4c70-ab72-b398e6da4346", "4cd6f68e-bf4d-42c7-8ed1-16eda41001aa", "Teacher", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "207717ea-38e2-41bb-8b71-b05c39ce9e85", "77cc782e-598d-4230-8f2f-cf4e02399a8c", "Student", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "207717ea-38e2-41bb-8b71-b05c39ce9e85");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21f4c81b-080b-4c70-ab72-b398e6da4346");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c289ef29-d7fc-4884-a694-2183fe9ab285");

            migrationBuilder.DropColumn(
                name: "RightStudent",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "TestResultQuestionId",
                table: "Answers");

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
        }
    }
}
