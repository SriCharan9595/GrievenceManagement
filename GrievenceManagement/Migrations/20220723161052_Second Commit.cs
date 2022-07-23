using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrievenceManagement.Migrations
{
    public partial class SecondCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Defendent",
                table: "IssueData",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defendent",
                table: "IssueData");
        }
    }
}
