using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Pages.Admin.Cases
{
    public class IndexModel : PageModel
    {
        private readonly ICaseService _caseService;

        public IndexModel(ICaseService caseService)
        {
            _caseService = caseService;
        }

        public IReadOnlyList<Models.Case> Cases { get; set; }

        public async Task OnGetAsync()
        {
            Cases = await _caseService.GetOrderedCasesAsync(includeReports: true);
        }
    }
}