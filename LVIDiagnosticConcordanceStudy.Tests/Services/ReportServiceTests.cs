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
            Assert.Equal(latestTestreport.Statistics.BayesForAge, Decimal.Round(currentReport.Statistics.BayesForAge, 9));

        }
    }
}
