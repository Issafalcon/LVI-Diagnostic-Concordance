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
        
        public async Task<IReadOnlyList<Report>> GetUserReports(string userId)
        {
            var reportFilter = new ReportFilterSpecification(userId, null);

            return await _reportRepository.ListAsync(reportFilter);
        }

        public async Task CreateReportFromCase(Case currentCase, Grade grade, int numberOfLVI, string userId)
        {
            Report previousReport = GetPreviousUserReport(userId);
            ReportStatistics statistics = await Task.Run(() => CalculateStatistics(currentCase.PatientAge, currentCase.TumourSize, grade, numberOfLVI, previousReport));

            Report newReport = new Report(previousReport.UserReportNumber + 1, userId, currentCase.Id, statistics);
            await _reportRepository.AddAsync(newReport);
        }

        private ReportStatistics CalculateStatistics(int ptAge, decimal tumourSize, Grade grade, int numLVISeen, Report previousReport)
        {
            ReportStatistics statistics = new ReportStatistics();

            statistics.LVIPresent = numLVISeen > 0;
            statistics.ProbLVIPos50Plus = ptAge < 51 ? DataConstants.BelowFiftyYearsLVIPos : 1 - DataConstants.BelowFiftyYearsLVIPos;
            statistics.ProbLVINeg50Plus = ptAge < 51 ? DataConstants.BelowFiftyYearsLVINeg : 1 - DataConstants.BelowFiftyYearsLVINeg;
            statistics.BayesForAge = CalculateBayes(DataConstants.ProbLVIPos,
                DataConstants.BelowFiftyYearsLVIPos,
                DataConstants.BelowFiftyYearsLVINeg,
                DataConstants.ProbLVINeg);

            statistics.ProbLVIPosSize = tumourSize > 2 ? DataConstants.TwoToFivecmLVIPos
                : tumourSize > 1 ? DataConstants.OneToTwocmLVIPos
                : DataConstants.BelowOnecmLVIPos;
            statistics.ProbLVINegSize = tumourSize > 2 ? DataConstants.TwoToFivecmLVINeg
                : tumourSize > 1 ? DataConstants.OneToTwocmLVINeg
                : DataConstants.BelowOnecmLVINeg;
            statistics.BayesForSize = CalculateBayes
                (statistics.BayesForAge, 
                 statistics.ProbLVIPosSize,
                 statistics.ProbLVINegSize, 
                 1 - statistics.BayesForAge);

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

            // Calculate the cumulative values based on results of previously submitted cases
            statistics.CumulativeBayesForSize = previousReport.Statistics.BayesForSize + statistics.BayesForSize;
            statistics.CumulativeAverageBayesForSize = statistics.CumulativeBayesForSize / (previousReport.UserReportNumber + 1);
            statistics.CumulativeCasesWithLVIPos = previousReport.Statistics.CumulativeCasesWithLVIPos + (statistics.LVIPresent ? 1 : 0);

            var binomDist = new BinomialDistribution((previousReport.UserReportNumber + 1), (double)statistics.CumulativeAverageBayesForSize);
            statistics.BinomialDist = (decimal)binomDist.DistributionFunction(statistics.CumulativeCasesWithLVIPos);
            return statistics;
        }

        private decimal CalculateBayes(decimal baseProb, decimal positivePredictiveProb, decimal negativePredictiveProb, decimal baseNegativePredictiveProb)
        {
            decimal posTimesBase = positivePredictiveProb * baseProb;

            return (posTimesBase) / (posTimesBase + (negativePredictiveProb * baseNegativePredictiveProb));
        }

        private Report GetPreviousUserReport(string userId)
        {
            // Only base cumulative calculations off reports that have been completely submitted
            var reportFilter = new ReportFilterSpecification(userId, null, orderByReportNumberDesc: false);
            Report previousReport = _reportRepository.GetPreviousReportForUser(userId);

            return previousReport;
        }
    }
}
