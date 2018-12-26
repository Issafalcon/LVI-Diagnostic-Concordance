using System.Linq;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Univariate;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using LVIDiagnosticConcordanceStudy.Services.Domain;

namespace LVIDiagnosticConcordanceStudy.Services.ViewModel
{
    public class CaseReportViewModelService : ICaseReportViewModelService
    {
        private readonly IAsyncRepository<Case> _caseRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IReportService _reportService;

        public CaseReportViewModelService(
            IAsyncRepository<Case> caseRepository,
            IReportRepository reportRepository,
            IReportService reportService)
        {
            _caseRepository = caseRepository;
            _reportRepository = reportRepository;
            _reportService = reportService;
        }

        public async Task<CaseReportViewModel> GetCaseReportForUser(string userId, int caseId)
        {
            
            Case currentCase = await _caseRepository.GetByIdAsync(caseId);

            Report report = GetExistingReport(userId, caseId);

            if (currentCase == null)
            {
                return null;
            }

            CaseReportViewModel caseReport = new CaseReportViewModel
            {
                PatientAge = currentCase.PatientAge,
                TumourSize = currentCase.TumourSize,
                IsSubmitted = false
            };

            if (report != null)
            {
                caseReport.TumourGrade = report.TumourGrade;
                caseReport.NumberofLVI = report.NumberofLVI;
                caseReport.IsSubmitted = report.IsSubmitted;
            }

            return caseReport;
        }

        public Report GetExistingReport (string userId, int caseId)
        {
            var reportFilter = new ReportFilterSpecification(userId, caseId);
            return _reportRepository.GetSingleBySpec(reportFilter);
        }

        public async Task CreateOrUpdateCaseReport(CaseReportViewModel caseReport, Report existingReport, int caseId, string userId, bool isSubmitted = false)
        {
            Case currentCase = await _caseRepository.GetByIdAsync(caseId);
            await _reportService.CreateOrUpdateReportFromCase(currentCase, existingReport, caseReport.TumourGrade, caseReport.NumberofLVI, userId, isSubmitted);
        }

        public async Task<int> GetCaseCount()
        {
            var allCasesSpecification = new CaseFilterSpecification(null);
            return await _caseRepository.CountAsync(allCasesSpecification);
        }

        public int[] GetSubmittedCaseReportIds(string userId)
        {
            return _reportRepository.GetSubmittedReportIdsForUser(userId);
        }

        public async Task<ChartValues> GetChartValuesForCaseReport(CaseReportViewModel caseReportViewModel, int caseId, string userId)
        {
            int[] chartXAxis;
            decimal[] theoreticalYValues;
            decimal[] observedYValues;

            Report previousReport = _reportService.GetPreviousUserReport(userId);

            int currentReportNumber = previousReport != null ? previousReport.UserReportNumber + 1 : 1; ;

            // Calculate statistics for the current case report on the fly
            ReportStatistics currentStatistics = await Task.Run(() => _reportService.CalculateStatistics(
                caseReportViewModel.PatientAge,
                caseReportViewModel.TumourSize,
                caseReportViewModel.TumourGrade,
                caseReportViewModel.NumberofLVI,
                previousReport));

            // Get the theoretical series values (x and y) 
            theoreticalYValues = new decimal[currentReportNumber];
            observedYValues = new decimal[currentReportNumber];
            chartXAxis = new int[currentReportNumber];

            BinomialDistribution binomDist = new BinomialDistribution(currentReportNumber, (double)currentStatistics.CumulativeAverageBayesForGrade);

            if (currentReportNumber == 1)
            {
                // If we only have one cumulative value, then theoretical should be the same as observed
                theoreticalYValues[0] = (decimal)binomDist.ProbabilityMassFunction(1);
                observedYValues[0] = currentStatistics.BinomialDist;
            }
            else
            {
                for (int i = 0; i < currentReportNumber; i++)
                {
                    theoreticalYValues[i] = (decimal)binomDist.ProbabilityMassFunction(i);
                    observedYValues[i] = currentStatistics.CumulativeCasesWithLVIPos == i ? currentStatistics.BinomialDist : 0;
                    chartXAxis[i] = i;
                }
            }
            

            //if (currentStatistics.CumulativeCasesWithLVIPos == currentReportNumber - 1 || previousReport == null)
            //{
            //    observedYValue = currentStatistics.BinomialDist;
            //    observedXValue = currentReportNumber - 1;
            //}
            //else
            //{
            //    var observedReportFilter = new ChartObservedReportValuesSpecification(currentStatistics.CumulativeCasesWithLVIPos);
            //    Report observedReportValue = _reportRepository.GetSingleBySpec(observedReportFilter);

            //    observedYValue = observedReportValue.Statistics.BinomialDist;
            //    observedXValue = observedReportValue.UserReportNumber - 1;
            //}

            return new ChartValues(theoreticalYValues, chartXAxis, observedYValues);
        }
    }
}
