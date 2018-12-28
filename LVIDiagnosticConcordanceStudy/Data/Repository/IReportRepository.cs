using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Data.Repository
{
    public interface IReportRepository : IRepository<Report>, IAsyncRepository<Report>
    {
        Report GetPreviousSubmittedReportForUser(string userId);
        int[] GetSubmittedReportIdsForUser(string userId);
    }
}
