using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LVIDiagnosticConcordanceStudy.Pages.Admin.Cases
{
    public class EditModel : PageModel
    {
        private IAsyncRepository<Models.Case> _caseRepository;

        public EditModel(IAsyncRepository<Models.Case> caseRepository)
        {
            _caseRepository = caseRepository;
        }

        [BindProperty]
        public Models.Case Case { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Case = await _caseRepository.GetByIdAsync(id.Value);

            if (Case == null)
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

            var caseToUpdate = await _caseRepository.GetByIdAsync(id.Value);

            if (await TryUpdateModelAsync<Models.Case>(
                caseToUpdate,
                "case",
                c => c.PatientAge, c => c.TumourSize, c => c.SlideURL))
            {
                await _caseRepository.UpdateAsync(caseToUpdate);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}