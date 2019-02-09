using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using LVIDiagnosticConcordanceStudy.Services.Domain;
using LVIDiagnosticConcordanceStudy.Services.ViewModel;
using LVIDiagnosticConcordanceStudy.Tests.TestData;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LVIDiagnosticConcordanceStudy.Tests.Services
{
    public class CaseReportViewModelServiceTests
    {       

        [Theory]
        [ClassData(typeof(CaseReportData))]
        public async void CalculateCumulativeStatistics_CalculatesCorrectly(Report[] testReports)
        {
            // Arrange
            Report currentTestReport = testReports[testReports.Length - 1];
            Report previousTestReport = null;

            if (testReports.Length > 1)
            {
                previousTestReport = testReports[testReports.Length - 2];
            }

            var mock = new Mock<IReportService>();
            mock.Setup(rs => rs.GetPreviousUserReport(It.IsAny<string>())).Returns(previousTestReport);
            mock.Setup(rs => rs.CalculateStatistics(
                It.IsAny<int>(),
                It.IsAny<decimal>(),
                It.IsAny<Grade>(),
                It.IsAny<int>(),
                It.IsAny<Report>())).Returns(currentTestReport.Statistics);

            
            CaseReportViewModel model = new CaseReportViewModel()
            {
                PatientAge = currentTestReport.Case.PatientAge,
                TumourSize = currentTestReport.Case.TumourSize,
                TumourGrade = currentTestReport.TumourGrade,
                NumberofLVI = currentTestReport.NumberofLVI
            };

            InterventionData expectedData = InterventionTestData.OrderedInterventionDataSet[testReports.Length - 1];

            //Act
            CaseReportViewModelService crvmService = new CaseReportViewModelService(null, null, mock.Object);
            InterventionData actualData = await crvmService.GetInterventionDataForCaseReport(model, "testUser");

            //Assert
            for (int i = 0; i < actualData.ChartXAxis.Length; i++)
            {
                Assert.Equal(Decimal.Round(expectedData.ObservedYValues[i], 5), Decimal.Round(actualData.ObservedYValues[i], 5));
                Assert.Equal(Decimal.Round(expectedData.TheoreticalYValues[i], 5), Decimal.Round(actualData.TheoreticalYValues[i], 5));
            }
            Assert.Equal(Decimal.Round(expectedData.PreTestProbability, 5), Decimal.Round(actualData.PreTestProbability, 5));
            Assert.Equal(Decimal.Round(expectedData.PostTestProbability, 5), Decimal.Round(actualData.PostTestProbability, 5));

        }
    }
}
