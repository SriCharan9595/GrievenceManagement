using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrievenceManagement.Migrations
{
    public partial class FifthCOmmit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "IssueData",
                newName: "EmpDesignation");

            migrationBuilder.AddColumn<string>(
                name: "DefDesignation",
                table: "IssueData",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefDesignation",
                table: "IssueData");

            migrationBuilder.RenameColumn(
                name: "EmpDesignation",
                table: "IssueData",
                newName: "Designation");
        }
    }
}
