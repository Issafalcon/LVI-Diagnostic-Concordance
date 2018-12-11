using LVIDiagnosticConcordanceStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services.Domain
{
    public interface IReportService
    {
        Task<IReadOnlyList<Report>> GetUserReports(string userId);
        Task CreateReportFromCase(Case currentCase, Grade grade, int numberOfLVI, string userId);
    }
}
