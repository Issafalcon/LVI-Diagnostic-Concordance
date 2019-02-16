using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LVIDiagnosticConcordanceStudy.Data;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Services.ViewModel;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace LVIDiagnosticConcordanceStudy.Pages
{
    public class CaseModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<LVIStudyUser> _userManager;
        private readonly ICaseReportViewModelService _caseReportService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CaseModel(
            ApplicationDbContext context,
            UserManager<LVIStudyUser> userManager,
            ICaseReportViewModelService caseReportService,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _context = context;
            _userManager = userManager;
            _caseReportService = caseReportService;
            _sharedLocalizer = sharedLocalizer;
        }

        public LVIStudyUser CurrentUser { get; set; }

        [BindProperty]
        public CaseReportViewModel CaseReportViewModel { get; set; }

        public IReadOnlyList<Models.Case> Cases { get; private set; }
        public int CaseId { get; set; }
        public int[] SubmittedReports { get; private set; }
        public bool SubmitOnPost { get; set; } = false;
        public InterventionData InterventionData { get; set; }
        private ReportStatistics _statistics = new ReportStatistics();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurrentUser = await _userManager.GetUserAsync(User);
            CaseId = id.HasValue ? id.Value : 0;

            if (CurrentUser == null)
            {
                return NotFound();
            }

            CaseReportViewModel = await _caseReportService.GetCaseReportForUser(CurrentUser.Id, id.Value);
            Cases = await _caseReportService.GetOrderedCasesAsync();

            SubmittedReports = _caseReportService.GetSubmittedCaseReportIds(CurrentUser.Id);

            if (CaseReportViewModel == null)
            {
                return RedirectToPage("./Summary");
            }

            return Page();
        }

        public async Task<IActionResult> OnGetAdditionalProbabilityDataAsync(int? id, [FromQuery]CaseReportViewModel caseReportData)
        {
            InterventionData = await _caseReportService.GetInterventionDataForCaseReport(caseReportData, _userManager.GetUserId(User));

            return new JsonResult(InterventionData);
        }

        public IActionResult OnGetAdditionalProbabilityViewComponentAsync(decimal preTestProb, decimal postTestProb, decimal observedValue, bool lviReported)
        {
            return ViewComponent("AdditionalProbability", new { preTestProbability = preTestProb, postTestProbability = postTestProb, observed = observedValue, lviReported });
        }

        public async Task<IActionResult> OnGetPreTestProbabilityDataAsync(int? id, [FromQuery]CaseReportViewModel caseReportData)
        {
            
            await _caseReportService.GetPreTestProbabilityData(caseReportData, _statistics);

            return new JsonResult(_statistics.BayesForGrade);
        }

        public IActionResult OnGetPreTestProbabilityViewComponentAsync(decimal preTestProb)
        {
            return ViewComponent("PreTestProbability", new { preTestProbability = preTestProb });
        }

        public async Task<IActionResult> OnPostAsync(int? id, bool isFromClient = false)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Report existingReport = _caseReportService.GetExistingReport(_userManager.GetUserId(User), id.Value);

            await _caseReportService.CreateOrUpdateCaseReport(CaseReportViewModel, existingReport, id.Value, _userManager.GetUserId(User), SubmitOnPost);

            if (isFromClient)
            {
                // Calling in from ajax, we need to return a redirect url so we can redirect from the client
                // as redirects do not get handled in ajax callback functions
                return new JsonResult(new { redirectUrl = "/Case/Index/" + (id.Value + 1).ToString() });
            }

            return RedirectToPage("/Case/Index", new { id = id.Value + 1 });
        }

        public async Task<IActionResult> OnPostSubmittedAsync(int? id, [FromQuery]bool isFromClient = false)
        {
            SubmitOnPost = true;
            return await OnPostAsync(id, isFromClient);
        }
    }
}
