using Microsoft.EntityFrameworkCore.Migrations;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    public partial class correctedcumulativestatisticcolumnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CumulativeAverageBayesForSize",
                table: "ReportStatistics");

            migrationBuilder.DropColumn(
                name: "CumulativeBayesForSize",
                table: "ReportStatistics");

            migrationBuilder.AddColumn<double>(
                name: "CumulativeAverageBayesForGrade",
                table: "ReportStatistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CumulativeBayesForGrade",
                table: "ReportStatistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CumulativeAverageBayesForGrade",
                table: "ReportStatistics");

            migrationBuilder.DropColumn(
                name: "CumulativeBayesForGrade",
                table: "ReportStatistics");

            migrationBuilder.AddColumn<double>(
                name: "CumulativeAverageBayesForSize",
                table: "ReportStatistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CumulativeBayesForSize",
                table: "ReportStatistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
