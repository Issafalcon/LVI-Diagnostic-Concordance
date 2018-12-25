using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class IsSubmittedcolumnaddedtoreports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "Reports",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "Reports");
        }
    }
}
