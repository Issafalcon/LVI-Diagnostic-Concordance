using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class report_user_relationship_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "LVIStudyUserId",
                table: "Reports",
                newName: "LVIStudyUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_LVIStudyUserId",
                table: "Reports",
                newName: "IX_Reports_LVIStudyUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserID",
                table: "Reports",
                column: "LVIStudyUserID",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserID",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "LVIStudyUserID",
                table: "Reports",
                newName: "LVIStudyUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_LVIStudyUserID",
                table: "Reports",
                newName: "IX_Reports_LVIStudyUserId");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserId",
                table: "Reports",
                column: "LVIStudyUserId",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
