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
        Task CreateCaseReport(CaseReportViewModel caseReport, int caseId, string userId);
    }
}
