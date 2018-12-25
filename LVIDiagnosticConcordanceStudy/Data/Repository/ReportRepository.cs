using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Data.Repository
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public Report GetPreviousSubmittedReportForUser(string userId)
        {
            var userReports = from report in _dbContext.Report
                              where report.LVIStudyUserID == userId && report.IsSubmitted == true
                              orderby report.UserReportNumber descending
                              select report;

            return userReports.FirstOrDefault();                              
        }

        public int[] GetSubmittedReportIdsForUser(string userId)
        {
            var userReports = from report in _dbContext.Report
                              where report.LVIStudyUserID == userId && report.IsSubmitted == true
                              orderby report.UserReportNumber descending
                              select report.CaseId;

            return userReports.ToArray();
        }
    }
}
