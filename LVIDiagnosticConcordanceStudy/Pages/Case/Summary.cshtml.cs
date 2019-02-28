using System.Collections.Generic;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
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
            LVIStudyUser user = await _userManager.GetUserAsync(User);

            if (user == null || user.CompleteStudy)
            {
                return NotFound();
            }
            Cases = await _caseReportService.GetOrderedCasesAsync();

            Reports = await _reportService.GetUserReportsOrderedByCase(user.Id);
            SubmittedReports = new List<int>();
            foreach (var report in Reports)
            {
                SubmittedReports.Add(report.CaseId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostResetReportsAsync()
        {
            await _reportService.DeleteUserReports(_userManager.GetUserId(User));
            return RedirectToPage("./Summary");
        }

        public async Task<IActionResult> OnPostCompleteStudyAsync()
        {
            LVIStudyUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.CompleteStudy = true;

            await _userManager.UpdateAsync(user);
            return RedirectToPage("../Index");
        }
    }
}