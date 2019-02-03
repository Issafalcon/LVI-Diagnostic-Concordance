using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using LVIDiagnosticConcordanceStudy.Services.Domain;
using LVIDiagnosticConcordanceStudy.Services.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LVIDiagnosticConcordanceStudy.Pages.Case
{
    public class SummaryModel : PageModel
    {
        private readonly IReportService _reportService;
        private readonly ICaseReportViewModelService _caseReportService;
        private readonly UserManager<LVIStudyUser> _userManager;

        public SummaryModel(IReportService reportService, ICaseReportViewModelService caseReportService, UserManager<LVIStudyUser> userManager)
        {
            _reportService = reportService;
            _caseReportService = caseReportService;
            _userManager = userManager;
        }

        [BindProperty]
        public IReadOnlyList<Report> Reports { get; private set; }
        public IReadOnlyList<Models.Case> Cases { get; private set; }
        public List<int> SubmittedReports { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            Cases = await _caseReportService.GetOrderedCasesAsync();

            Reports = await _reportService.GetUserReportsOrderedByCase(userId);
            SubmittedReports = new List<int>();
            foreach (var report in Reports)
            {
                SubmittedReports.Add(report.CaseId);
            }

            return Page();
        }
    }
}