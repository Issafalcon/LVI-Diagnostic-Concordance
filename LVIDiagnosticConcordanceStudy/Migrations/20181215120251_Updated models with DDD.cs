using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class UpdatedmodelswithDDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Case_CaseID",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_AspNetUsers_UserID",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportStatistics_Report_ReportID",
                table: "ReportStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportStatistics",
                table: "ReportStatistics");

            migrationBuilder.DropIndex(
                name: "IX_ReportStatistics_ReportID",
                table: "ReportStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_UserID",
                table: "Report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Case",
                table: "Case");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReportStatisticsID",
                table: "ReportStatistics");

            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "ReportStatistics");

            migrationBuilder.DropColumn(
                name: "TheoreticalBinomialDist",
                table: "ReportStatistics");

            migrationBuilder.RenameTable(
                name: "Report",
                newName: "Reports");

            migrationBuilder.RenameTable(
                name: "Case",
                newName: "Cases");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "LVIStudyUser");

            migrationBuilder.RenameColumn(
                name: "ReportID",
                table: "ReportStatistics",
                newName: "ReportId");

            migrationBuilder.RenameColumn(
                name: "CaseID",
                table: "Reports",
                newName: "CaseId");

            migrationBuilder.RenameColumn(
                name: "ReportID",
                table: "Reports",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Report_CaseID",
                table: "Reports",
                newName: "IX_Reports_CaseId");

            migrationBuilder.RenameColumn(
                name: "CaseID",
                table: "Cases",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Reports",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "Reports",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LVIStudyUserId",
                table: "Reports",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TumourSize",
                table: "Cases",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5, 2)");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "LVIStudyUser",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportStatistics",
                table: "ReportStatistics",
                column: "ReportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cases",
                table: "Cases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LVIStudyUser",
                table: "LVIStudyUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_LVIStudyUserId",
                table: "Reports",
                column: "LVIStudyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_LVIStudyUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_LVIStudyUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_LVIStudyUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_LVIStudyUser_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Cases_CaseId",
                table: "Reports",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserId",
                table: "Reports",
                column: "LVIStudyUserId",
                principalTable: "LVIStudyUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportStatistics_Reports_ReportId",
                table: "ReportStatistics",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_LVIStudyUser_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_LVIStudyUser_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_LVIStudyUser_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_LVIStudyUser_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Cases_CaseId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_LVIStudyUser_LVIStudyUserId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportStatistics_Reports_ReportId",
                table: "ReportStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportStatistics",
                table: "ReportStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_LVIStudyUserId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LVIStudyUser",
                table: "LVIStudyUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cases",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "LVIStudyUserId",
                table: "Reports");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "Report");

            migrationBuilder.RenameTable(
                name: "LVIStudyUser",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Cases",
                newName: "Case");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "ReportStatistics",
                newName: "ReportID");

            migrationBuilder.RenameColumn(
                name: "CaseId",
                table: "Report",
                newName: "CaseID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Report",
                newName: "ReportID");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_CaseId",
                table: "Report",
                newName: "IX_Report_CaseID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Case",
                newName: "CaseID");

            migrationBuilder.AddColumn<int>(
                name: "ReportStatisticsID",
                table: "ReportStatistics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "ReportStatistics",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "TheoreticalBinomialDist",
                table: "ReportStatistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Report",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaseID",
                table: "Report",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<decimal>(
                name: "TumourSize",
                table: "Case",
                type: "decimal(5, 2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportStatistics",
                table: "ReportStatistics",
                column: "ReportStatisticsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report",
                table: "Report",
                column: "ReportID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Case",
                table: "Case",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportStatistics_ReportID",
                table: "ReportStatistics",
                column: "ReportID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_UserID",
                table: "Report",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Case_CaseID",
                table: "Report",
                column: "CaseID",
                principalTable: "Case",
                principalColumn: "CaseID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_AspNetUsers_UserID",
                table: "Report",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportStatistics_Report_ReportID",
                table: "ReportStatistics",
                column: "ReportID",
                principalTable: "Report",
                principalColumn: "ReportID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
