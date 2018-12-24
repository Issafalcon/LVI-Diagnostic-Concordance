using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LVIDiagnosticConcordanceStudy.Data;
using LVIDiagnosticConcordanceStudy.Models.Entities;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Services.ViewModel;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;

namespace LVIDiagnosticConcordanceStudy.Pages
{
    public class CaseModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<LVIStudyUser> _userManager;
        private readonly ICaseReportViewModelService _caseReportService;

        public CaseModel(
            ApplicationDbContext context,
            UserManager<LVIStudyUser> userManager,
            ICaseReportViewModelService caseReportService)
        {
            _context = context;
            _userManager = userManager;
            _caseReportService = caseReportService;
        }

        public LVIStudyUser CurrentUser { get; set; }

        [BindProperty]
        public CaseReportViewModel CaseReportViewModel { get; set; }
        public int CaseCount { get; private set; }
        public bool SubmitOnPost { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser == null)
            {
                return NotFound();
            }

            CaseReportViewModel = await _caseReportService.GetCaseReportForUser(CurrentUser.Id, id.Value);
            CaseCount = await _caseReportService.GetCaseCount();

            if (CaseReportViewModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Report existingReport = _caseReportService.GetExistingReport(CurrentUser.Id, id.Value);

            await _caseReportService.CreateOrUpdateCaseReport(CaseReportViewModel, existingReport, id.Value, CurrentUser.Id, SubmitOnPost);

            // TODO: Fix the redirect - Not working currently
            return RedirectToPage("Case/" + (id.Value + 1).ToString());
        }

        public async Task<IActionResult> OnPostSubmittedAsync(int? id)
        {
            SubmitOnPost = true;
            return await OnPostAsync(id);
        }

        public IActionResult OnGetChartVC(int id)
        {
            return ViewComponent("Chart", new { caseReportViewModel = CaseReportViewModel, caseId = id, userId = CurrentUser.Id });
        }
    }
}
