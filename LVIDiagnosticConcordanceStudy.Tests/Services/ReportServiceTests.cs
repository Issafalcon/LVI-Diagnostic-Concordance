using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Services;
using LVIDiagnosticConcordanceStudy.Tests.TestData;
using System;
using Xunit;
using Moq;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System.Collections.Generic;

namespace LVIDiagnosticConcordanceStudy.Tests
{
    public class ReportServiceTests
    {
        Mock reportRepositoryMock = new Mock<IReportRepository>();
        Mock caseRepositoryMock = new Mock<IAsyncRepository<Case>>();

        [Theory]
        [ClassData(typeof(CaseReportData))]
        public void CalculateAgeRelatedStatistics_CalculatesCorrectly(Report[] testReports)
        {
            //Arrange
            var reportService = new ReportService(null, null);
            Report latestTestreport = testReports[testReports.Length -1];

            Report currentReport = new Report(latestTestreport.UserReportNumber, latestTestreport.LVIStudyUserID, latestTestreport.CaseId, new ReportStatistics(), false);
            currentReport.Case = latestTestreport.Case;

            //Act
            reportService.CalculateAgeBasedStatistics(currentReport.Statistics, currentReport.Case.PatientAge);

            //Assert
            Assert.Equal(latestTestreport.Statistics.BayesForAge, Decimal.Round(currentReport.Statistics.BayesForAge, 9));

        }

        [Theory]
        [ClassData(typeof(CaseReportData))]
        public void CalculateTumourSizeRelatedStatistics_CalculatesCorrectly(Report[] testReports)
        {
            //Arrange
            var reportService = new ReportService(null, null);
            Report latestTestreport = testReports[testReports.Length - 1];

            Report currentReport = new Report(latestTestreport.UserReportNumber, latestTestreport.LVIStudyUserID, latestTestreport.CaseId, new ReportStatistics(), false);
            currentReport.Statistics.BayesForAge = latestTestreport.Statistics.BayesForAge;
            currentReport.Case = latestTestreport.Case;

            //Act
            reportService.CalculateSizeBasedStatistics(currentReport.Statistics, currentReport.Case.TumourSize);

            //Assert
            Assert.Equal(latestTestreport.Statistics.BayesForSize, Decimal.Round(currentReport.Statistics.BayesForSize, 9));

        }

        [Theory]
        [ClassData(typeof(CaseReportData))]
        public void CalculateTumourGradeRelatedStatistics_CalculatesCorrectly(Report[] testReports)
        {
            //Arrange
            var reportService = new ReportService(null, null);
            Report latestTestreport = testReports[testReports.Length - 1];

            Report currentReport = new Report(latestTestreport.UserReportNumber, latestTestreport.LVIStudyUserID, latestTestreport.CaseId, new ReportStatistics(), false);
            currentReport.Statistics.BayesForAge = latestTestreport.Statistics.BayesForAge;
            currentReport.Statistics.BayesForSize = latestTestreport.Statistics.BayesForSize;
            currentReport.TumourGrade = latestTestreport.TumourGrade;
            currentReport.Case = latestTestreport.Case;

            //Act
            reportService.CalculatePreTestProbability(currentReport.Statistics, currentReport.TumourGrade);

            //Assert
            Assert.Equal(latestTestreport.Statistics.BayesForGrade, Decimal.Round(currentReport.Statistics.BayesForGrade, 9));

        }

        [Theory]
        [ClassData(typeof(CaseReportData))]
        public void CalculateNumOfLVIRelatedStatistics_CalculatesCorrectly(Report[] testReports)
        {
            //Arrange
            var reportService = new ReportService(null, null);
            Report latestTestreport = testReports[testReports.Length - 1];

            Report currentReport = new Report(latestTestreport.UserReportNumber, latestTestreport.LVIStudyUserID, latestTestreport.CaseId, new ReportStatistics(), false);
            currentReport.Statistics.BayesForAge = latestTestreport.Statistics.BayesForAge;
            currentReport.Statistics.BayesForSize = latestTestreport.Statistics.BayesForSize;
            currentReport.Statistics.BayesForGrade = latestTestreport.Statistics.BayesForGrade;
            currentReport.NumberofLVI = latestTestreport.NumberofLVI;
            currentReport.Case = latestTestreport.Case;

            //Act
            reportService.CalculatePostTestProbability(currentReport.Statistics, currentReport.NumberofLVI);

            //Assert
            Assert.Equal(latestTestreport.Statistics.BayesForNumberOfLVI, Decimal.Round(currentReport.Statistics.BayesForNumberOfLVI, 9));

        }
    }
}
