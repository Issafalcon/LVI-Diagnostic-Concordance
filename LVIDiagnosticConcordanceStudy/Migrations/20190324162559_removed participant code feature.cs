using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class removedparticipantcodefeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LVIStudyUser_ParticipantCode_Code",
                table: "LVIStudyUser");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ParticipantCode_Code",
                table: "ParticipantCode");

            migrationBuilder.DropIndex(
                name: "IX_LVIStudyUser_Code",
                table: "LVIStudyUser");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "LVIStudyUser");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ParticipantCode",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "LVIStudyUserId",
                table: "ParticipantCode",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantCode_LVIStudyUserId",
                table: "ParticipantCode",
                column: "LVIStudyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantCode_LVIStudyUser_LVIStudyUserId",
                table: "ParticipantCode",
                column: "LVIStudyUserId",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantCode_LVIStudyUser_LVIStudyUserId",
                table: "ParticipantCode");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantCode_LVIStudyUserId",
                table: "ParticipantCode");

            migrationBuilder.DropColumn(
                name: "LVIStudyUserId",
                table: "ParticipantCode");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ParticipantCode",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "LVIStudyUser",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ParticipantCode_Code",
                table: "ParticipantCode",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_LVIStudyUser_Code",
                table: "LVIStudyUser",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_LVIStudyUser_ParticipantCode_Code",
                table: "LVIStudyUser",
                column: "Code",
                principalTable: "ParticipantCode",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
