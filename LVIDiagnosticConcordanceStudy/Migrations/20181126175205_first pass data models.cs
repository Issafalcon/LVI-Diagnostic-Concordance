using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class firstpassdatamodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Culture",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InControlGroup",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBreastSpecialist",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfWork",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearsInPath",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsQualified",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Case",
                columns: table => new
                {
                    CaseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PatientAge = table.Column<int>(nullable: false),
                    TumourSize = table.Column<decimal>(type: "decimal(5, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.CaseID);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserReportNumber = table.Column<int>(nullable: false),
                    TumourGrade = table.Column<int>(nullable: false),
                    NumberofLVI = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    CaseID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_Report_Case_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Case",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Report_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportStatistics",
                columns: table => new
                {
                    ReportStatisticsID = table.Column<int>(nullable: false),
                    LVIPresent = table.Column<bool>(nullable: false),
                    ProbLVIPos50Plus = table.Column<double>(type: "float", nullable: false),
                    ProbLVINeg50Plus = table.Column<double>(type: "float", nullable: false),
                    BayesForAge = table.Column<double>(type: "float", nullable: false),
                    ProbLVIPosSize = table.Column<double>(type: "float", nullable: false),
                    ProbLVINegSize = table.Column<double>(type: "float", nullable: false),
                    BayesForSize = table.Column<double>(type: "float", nullable: false),
                    ProbLVIPosGrade = table.Column<double>(type: "float", nullable: false),
                    ProbLVINegGrade = table.Column<double>(type: "float", nullable: false),
                    BayesForGrade = table.Column<double>(type: "float", nullable: false),
                    ProbLVIPosNumberOfLVI = table.Column<double>(type: "float", nullable: false),
                    ProbLVINegNumberOfLVI = table.Column<double>(type: "float", nullable: false),
                    BayesForNumberOfLVI = table.Column<double>(type: "float", nullable: false),
                    CumulativeBayesForSize = table.Column<double>(type: "float", nullable: false),
                    CumulativeAverageBayesForSize = table.Column<double>(type: "float", nullable: false),
                    CumulativeCasesWithLVIPos = table.Column<int>(nullable: false),
                    BinomialDist = table.Column<double>(type: "float", nullable: false),
                    TheoreticalBinomialDist = table.Column<double>(type: "float", nullable: false),
                    IsSubmitted = table.Column<bool>(nullable: false),
                    ReportID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStatistics", x => x.ReportStatisticsID);
                    table.ForeignKey(
                        name: "FK_ReportStatistics_Report_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Report",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_CaseID",
                table: "Report",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_UserID",
                table: "Report",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportStatistics_ReportID",
                table: "ReportStatistics",
                column: "ReportID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportStatistics");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Case");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InControlGroup",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsBreastSpecialist",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlaceOfWork",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YearsInPath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "YearsQualified",
                table: "AspNetUsers");
        }
    }
}
