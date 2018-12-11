using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LVIDiagnosticConcordanceStudy.Data;
using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Services.ViewModel;

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
        public int CaseId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CaseReportViewModel = await _caseReportService.GetCaseReportForUser(_userManager.GetUserId(User), id.Value);
            CaseId = id.Value;

            if (CaseReportViewModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _caseReportService.CreateCaseReport(CaseReportViewModel, CaseId, _userManager.GetUserId(User));

            return RedirectToPage(CaseId + 1);
        }
    }
}
