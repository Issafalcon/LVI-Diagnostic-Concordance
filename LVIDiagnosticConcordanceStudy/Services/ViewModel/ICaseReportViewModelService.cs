using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services.ViewModel
{
    public interface ICaseReportViewModelService
    {
        Task<CaseReportViewModel> GetCaseReportForUser(string userId, int caseId);
        Report GetExistingReport(string userId, int caseId);
        Task CreateOrUpdateCaseReport(CaseReportViewModel caseReport, Report existingReport, int caseId, string userId, bool isSubmitted = false);
        Task<int> GetCaseCount();
    }
}
