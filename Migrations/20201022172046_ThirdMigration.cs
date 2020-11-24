using Microsoft.EntityFrameworkCore.Migrations;

namespace Exam.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HobbyDescription",
                table: "Hobbies",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HobbyDescription",
                table: "Hobbies");
        }
    }
}
