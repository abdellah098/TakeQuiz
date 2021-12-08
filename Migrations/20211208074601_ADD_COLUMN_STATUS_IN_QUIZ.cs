using Microsoft.EntityFrameworkCore.Migrations;

namespace Quiz_back.Migrations
{
    public partial class ADD_COLUMN_STATUS_IN_QUIZ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Quiz",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Quiz");
        }
    }
}
