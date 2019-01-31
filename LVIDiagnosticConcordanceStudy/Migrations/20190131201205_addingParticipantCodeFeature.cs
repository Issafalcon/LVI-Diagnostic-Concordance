using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class addingParticipantCodeFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParticipantCodeId",
                table: "LVIStudyUser",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Cases_CaseId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientAge = table.Column<int>(nullable: false),
                    TumourSize = table.Column<decimal>(type: "decimal(5, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Cases_CaseId",
                table: "Reports",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateTable(
                name: "ParticipantCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantCode", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LVIStudyUser_ParticipantCodeId",
                table: "LVIStudyUser",
                column: "ParticipantCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LVIStudyUser_ParticipantCode_ParticipantCodeId",
                table: "LVIStudyUser",
                column: "ParticipantCodeId",
                principalTable: "ParticipantCode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LVIStudyUser_ParticipantCode_ParticipantCodeId",
                table: "LVIStudyUser");

            migrationBuilder.DropTable(
                name: "ParticipantCode");

            migrationBuilder.DropIndex(
                name: "IX_LVIStudyUser_ParticipantCodeId",
                table: "LVIStudyUser");

            migrationBuilder.DropColumn(
                name: "ParticipantCodeId",
                table: "LVIStudyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Cases_CaseId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PatientAge = table.Column<int>(nullable: false),
                    TumourSize = table.Column<decimal>(type: "decimal(5, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Cases_CaseId",
                table: "Reports",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
