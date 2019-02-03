using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Univariate;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;

namespace LVIDiagnosticConcordanceStudy.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IAsyncRepository<Case> _caseRepository;

        public ReportService(
            IReportRepository reportRepository,
            IAsyncRepository<Case> caseRepository)
        {
            _reportRepository = reportRepository;
            _caseRepository = caseRepository;
        }
        
        public async Task<IReadOnlyList<Report>> GetUserReports(string userId, bool includeCase = false)
        {
            var reportFilter = new ReportFilterSpecification(userId, null, includeCase: includeCase);

            return await _reportRepository.ListAsync(reportFilter);
        }

        public async Task<IReadOnlyList<Report>> GetUserReportsOrderedByCase(string userId, bool includeCase = false)
        {
            var reportFilter = new ReportFilterSpecification(userId, null, includeCase: includeCase, orderByCaseNumber: true);

            return await _reportRepository.ListAsync(reportFilter);
        }

        public int[] GetSubmittedUserReportIds(string userId)
        {
            return _reportRepository.GetSubmittedReportIdsForUser(userId);
        }

        public async Task CreateOrUpdateReportFromCase(Case currentCase, Report report, Grade grade, int numberOfLVI, string userId, bool isSubmitted = false)
        {
            Report previousReport = GetPreviousUserReport(userId);
            ReportStatistics statistics = null;

            if (isSubmitted)
            {
               statistics  = await Task.Run(() => CalculateStatistics(currentCase.PatientAge, currentCase.TumourSize, grade, numberOfLVI, previousReport));
            }

            if (report == null)
            {
                int newReportNumber = previousReport != null ? previousReport.UserReportNumber + 1 : 1;
                Report newReport = new Report(newReportNumber, userId, currentCase.Id, statistics, isSubmitted);

                newReport.TumourGrade = grade;
                newReport.NumberofLVI = numberOfLVI;
                await _reportRepository.AddAsync(newReport);
            }
            else
            {
                report.TumourGrade = grade;
                report.NumberofLVI = numberOfLVI;
                report.Statistics = statistics;
                await _reportRepository.UpdateAsync(report);
            }
            
        }

        public ReportStatistics CalculateStatistics(int ptAge, decimal tumourSize, Grade grade, int numLVISeen, Report previousReport)
        {
            ReportStatistics statistics = new ReportStatistics();

            // The four sets of Bayes theorum calculations need to be performed sequentially for the entire statistics set to be correct
            CalculateAgeBasedStatistics(statistics, ptAge);

            CalculateSizeBasedStatistics(statistics, tumourSize);

            CalculatePreTestProbability(statistics, grade);

            CalculatePostTestProbability(statistics, numLVISeen);

            // Calculate the cumulative values based on results of previously submitted cases
            int numberOfUserReports = previousReport != null ? previousReport.UserReportNumber + 1 : 1;

            if (previousReport != null)
            {
                statistics.CumulativeBayesForGrade = previousReport.Statistics.CumulativeBayesForGrade + statistics.BayesForGrade;
                statistics.CumulativeAverageBayesForGrade = statistics.CumulativeBayesForGrade / numberOfUserReports;
                statistics.CumulativeCasesWithLVIPos = previousReport.Statistics.CumulativeCasesWithLVIPos + (statistics.LVIPresent ? 1 : 0);             
            }
            else
            {                
                statistics.CumulativeBayesForGrade = statistics.BayesForGrade;
                statistics.CumulativeAverageBayesForGrade = statistics.CumulativeBayesForGrade / numberOfUserReports;
                statistics.CumulativeCasesWithLVIPos = statistics.LVIPresent ? 1 : 0;
            }

            BinomialDistribution binomDist = new BinomialDistribution(numberOfUserReports, (double)statistics.CumulativeAverageBayesForGrade);
            statistics.BinomialDist = (decimal)binomDist.ProbabilityMassFunction(statistics.CumulativeCasesWithLVIPos);
            return statistics;
        }

        public void CalculateAgeBasedStatistics(ReportStatistics statistics, int ptAge)
        {
            statistics.ProbLVIPos50Plus = ptAge < 51 ? DataConstants.BelowFiftyYearsLVIPos : 1 - DataConstants.BelowFiftyYearsLVIPos;
            statistics.ProbLVINeg50Plus = ptAge < 51 ? DataConstants.BelowFiftyYearsLVINeg : 1 - DataConstants.BelowFiftyYearsLVINeg;
            statistics.BayesForAge = CalculateBayes(DataConstants.ProbLVIPos,
                statistics.ProbLVIPos50Plus,
                statistics.ProbLVINeg50Plus,
                DataConstants.ProbLVINeg);
        }

        public void CalculateSizeBasedStatistics(ReportStatistics statistics, decimal tumourSize)
        {
            statistics.ProbLVIPosSize = tumourSize > (decimal)2 ? DataConstants.TwoToFivecmLVIPos
                : tumourSize > (decimal)1 ? DataConstants.OneToTwocmLVIPos
                : DataConstants.BelowOnecmLVIPos;
            statistics.ProbLVINegSize = tumourSize > (decimal)2 ? DataConstants.TwoToFivecmLVINeg
                : tumourSize > (decimal)1 ? DataConstants.OneToTwocmLVINeg
                : DataConstants.BelowOnecmLVINeg;
            statistics.BayesForSize = CalculateBayes
                (statistics.BayesForAge,
                 statistics.ProbLVIPosSize,
                 statistics.ProbLVINegSize,
                 1 - statistics.BayesForAge);
        }

        public void CalculatePreTestProbability(ReportStatistics statistics, Grade grade)
        {
            statistics.ProbLVIPosGrade = (int)grade == 1 ? DataConstants.GradeOneLVIPos
                : (int)grade == 2 ? DataConstants.GradeTwoLVIPos
                : DataConstants.GradeThreeLVIPos;
            statistics.ProbLVINegGrade = (int)grade == 1 ? DataConstants.GradeOneLVINeg
                : (int)grade == 2 ? DataConstants.GradeTwoLVINeg
                : DataConstants.GradeThreeLVINeg;
            statistics.BayesForGrade = CalculateBayes
                (statistics.BayesForSize,
                 statistics.ProbLVIPosGrade,
                 statistics.ProbLVINegGrade,
                 1 - statistics.BayesForSize);
        }

        public void CalculatePostTestProbability(ReportStatistics statistics, int numLVISeen)
        {
            statistics.LVIPresent = numLVISeen > 0;

            statistics.ProbLVIPosNumberOfLVI = numLVISeen == 0 ? DataConstants.ZeroLVIImagesLVIPos
                : numLVISeen < 3 ? DataConstants.OneToTwoLVIImagesLVIPos
                : numLVISeen < 4 ? DataConstants.TwoToThreeLVIImagesLVIPos
                : DataConstants.FivePlusLVIImagesLVIPos;
            statistics.ProbLVINegNumberOfLVI = numLVISeen == 0 ? DataConstants.ZeroLVIImagesLVINeg
                : numLVISeen < 3 ? DataConstants.OneToTwoLVIImagesLVINeg
                : numLVISeen < 4 ? DataConstants.TwoToThreeLVIImagesLVINeg
                : DataConstants.FivePlusLVIImagesLVINeg;
            statistics.BayesForNumberOfLVI = CalculateBayes
                (statistics.BayesForGrade,
                 statistics.ProbLVIPosNumberOfLVI,
                 statistics.ProbLVINegNumberOfLVI,
                 1 - statistics.BayesForGrade);
        }

        private decimal CalculateBayes(decimal baseProb, decimal positivePredictiveProb, decimal negativePredictiveProb, decimal baseNegativePredictiveProb)
        {
            decimal posTimesBase = positivePredictiveProb * baseProb;

            return (posTimesBase) / (posTimesBase + (negativePredictiveProb * baseNegativePredictiveProb));
        }

        public Report GetPreviousUserReport(string userId)
        {
            // Only base cumulative calculations off reports that have been completely submitted
            Report previousReport = _reportRepository.GetPreviousSubmittedReportForUser(userId);

            return previousReport;
        }
    }
}
