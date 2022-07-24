using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrievenceManagement.Migrations
{
    public partial class EighthCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmpName",
                table: "IssueData",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IssueData",
                keyColumn: "EmpName",
                keyValue: null,
                column: "EmpName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "EmpName",
                table: "IssueData",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }
    }
}
