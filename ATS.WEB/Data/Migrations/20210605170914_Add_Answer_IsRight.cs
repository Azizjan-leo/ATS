using Microsoft.EntityFrameworkCore.Migrations;

namespace ATS.WEB.Migrations
{
    public partial class Add_Answer_IsRight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRight",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRight",
                table: "Answers");
        }
    }
}
