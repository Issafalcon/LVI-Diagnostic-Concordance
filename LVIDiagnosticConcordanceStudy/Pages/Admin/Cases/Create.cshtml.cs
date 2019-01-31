using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LVIDiagnosticConcordanceStudy.Pages.Admin.Cases
{
    public class CreateModel : PageModel
    {
        private IAsyncRepository<Models.Case> _caseRepository;

        public CreateModel(IAsyncRepository<Models.Case> caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Case Case { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newCase = new Models.Case();

            if (await TryUpdateModelAsync<Models.Case>(
                newCase,
                "case",
                c => c.PatientAge, c => c.TumourSize))
            {
                await _caseRepository.AddAsync(newCase);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}