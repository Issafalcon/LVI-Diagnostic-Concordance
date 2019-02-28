using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class addingcasenumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CaseNumber",
                table: "Cases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseNumber",
                table: "Cases",
                column: "CaseNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cases_CaseNumber",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CaseNumber",
                table: "Cases");
        }
    }
}
