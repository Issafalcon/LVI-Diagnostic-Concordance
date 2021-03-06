using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
                c => c.CaseNumber, c => c.PatientAge, c => c.TumourSize, c => c.SlideURL))
            {
                try
                {
                    await _caseRepository.AddAsync(newCase);
                }
                catch (DbUpdateException e)
                {
                    ModelState.AddModelError("", e.InnerException.Message);
                    return Page();
                }

                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}