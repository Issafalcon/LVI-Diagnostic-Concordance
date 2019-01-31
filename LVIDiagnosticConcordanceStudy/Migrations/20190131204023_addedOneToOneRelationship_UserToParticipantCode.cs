using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class addedOneToOneRelationship_UserToParticipantCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LVIStudyUser_ParticipantCodeId",
                table: "LVIStudyUser");

            migrationBuilder.CreateIndex(
                name: "IX_LVIStudyUser_ParticipantCodeId",
                table: "LVIStudyUser",
                column: "ParticipantCodeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LVIStudyUser_ParticipantCodeId",
                table: "LVIStudyUser");

            migrationBuilder.CreateIndex(
                name: "IX_LVIStudyUser_ParticipantCodeId",
                table: "LVIStudyUser",
                column: "ParticipantCodeId");
        }
    }
}
