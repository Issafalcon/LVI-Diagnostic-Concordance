using Accord.Statistics.Distributions.Univariate;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewComponents;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using LVIDiagnosticConcordanceStudy.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services.ViewComponent
{
    public class ChartService : IChartService
    {
        private readonly IReportService _reportService;
        private readonly IReportRepository _reportRepository;

        public ChartService(IReportService reportService, IReportRepository reportRepository)
        {
            _reportService = reportService;
            _reportRepository = reportRepository;
        }

        public async Task<ChartValues> GetChartValuesForCaseReport(CaseReportViewModel caseReportViewModel, Report previousReport, int caseId, string userId)
        {
            decimal[] theoreticalYSeries;
            int[] chartXAxis;
            decimal observedYValue;
            int observedXValue;

            int currentReportNumber = previousReport != null ? previousReport.UserReportNumber + 1 : 1; ;

            // Calculate statistics for the current case report on the fly
            ReportStatistics currentStatistics = await Task.Run(() =>_reportService.CalculateStatistics(
                caseReportViewModel.PatientAge,
                caseReportViewModel.TumourSize,
                caseReportViewModel.TumourGrade,
                caseReportViewModel.NumberofLVI,
                previousReport));

            // Get the theoretical series values (x and y) 
            theoreticalYSeries = new decimal[currentReportNumber];
            chartXAxis = new int[currentReportNumber];

            BinomialDistribution binomDist = new BinomialDistribution(currentReportNumber, (double)currentStatistics.CumulativeAverageBayesForGrade);
            for (int i = 0; i <= currentReportNumber - 1; i++)
            {                
                theoreticalYSeries.Append((decimal)binomDist.ProbabilityMassFunction(i));
                chartXAxis.Append(i);
            }

            if (currentStatistics.CumulativeCasesWithLVIPos == currentReportNumber - 1 || previousReport == null)
            {
                observedYValue = currentStatistics.BinomialDist;
                observedXValue = currentReportNumber - 1;
            }
            else
            {
                var observedReportFilter = new ChartObservedReportValuesSpecification(currentStatistics.CumulativeCasesWithLVIPos);
                Report observedReportValue = _reportRepository.GetSingleBySpec(observedReportFilter);

                observedYValue = observedReportValue.Statistics.BinomialDist;
                observedXValue = observedReportValue.UserReportNumber - 1;
            }

            return new ChartValues(theoreticalYSeries, chartXAxis, observedYValue, observedXValue);
        }

    }
}
