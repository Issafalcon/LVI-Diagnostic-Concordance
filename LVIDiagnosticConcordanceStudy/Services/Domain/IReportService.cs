using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services.Domain
{
    public interface IReportService
    {
        Task<IReadOnlyList<Report>> GetUserReports(string userId, bool includeCase = false);
        Task<IReadOnlyList<Report>> GetUserReportsOrderedByCase(string userId, bool includeCase = false);
        Report GetPreviousUserReport(string userId);
        Task DeleteUserReports(string userId);
        Task CreateOrUpdateReportFromCase(Case currentCase, Report report, Grade grade, int numberOfLVI, string userId, bool isSubmitted = false);
        ReportStatistics CalculateStatistics(int ptAge, decimal tumourSize, Grade grade, int numLVISeen, Report previousReport);
        void CalculateAgeBasedStatistics(ReportStatistics statistics, int ptAge);
        void CalculateSizeBasedStatistics(ReportStatistics statistics, decimal tumourSize);
        void CalculatePreTestProbability(ReportStatistics statistics, Grade grade);
        void CalculatePostTestProbability(ReportStatistics statistics, int numLVISeen);
    }
}
