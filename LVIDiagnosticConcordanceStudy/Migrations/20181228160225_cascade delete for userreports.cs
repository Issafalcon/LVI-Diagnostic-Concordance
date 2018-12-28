using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class cascadedeleteforuserreports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserID",
                table: "Reports");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserID",
                table: "Reports",
                column: "LVIStudyUserID",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserID",
                table: "Reports");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserID",
                table: "Reports",
                column: "LVIStudyUserID",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
