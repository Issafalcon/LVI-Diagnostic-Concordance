using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LVIDiagnosticConcordanceStudy.Pages.Admin.Cases
{
    public class DeleteModel : PageModel
    {
        private IAsyncRepository<Models.Case> _caseRepository;

        public DeleteModel(IAsyncRepository<Models.Case> caseRepository)
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
            if (id == null)
            {
                return NotFound();
            }

            Case = await _caseRepository.GetByIdAsync(id.Value);

            if (Case != null)
            {
                await _caseRepository.DeleteAsync(Case);
            }

            return RedirectToPage("./Index");
        }
    }
}