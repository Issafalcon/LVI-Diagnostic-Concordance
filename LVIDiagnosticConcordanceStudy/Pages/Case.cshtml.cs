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

namespace LVIDiagnosticConcordanceStudy.Pages
{
    public class CaseModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<LVIStudyUser> _userManager;

        public CaseModel(
            ApplicationDbContext context,
            UserManager<LVIStudyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public LVIStudyUser CurrentUser { get; set; }

        [BindProperty]
        public CaseVM CaseViewModel { get; set; }
        public int CaseId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CaseViewModel = new CaseVM();
            Case currentCase = await _context.Case.FindAsync(id);

            if (currentCase == null)
            {
                return NotFound();
            }

            Report currentUserReport = await _context.Report
                                            .FirstOrDefaultAsync(r => r.UserID == _userManager.GetUserId(User) && r.Case.CaseID == id);

            CaseViewModel = new CaseVM
            {
                PatientAge = currentCase.PatientAge,
                TumourSize = currentCase.TumourSize
            };

            if (currentUserReport != null)
            {
                CaseViewModel.TumourGrade = currentUserReport.TumourGrade;
                CaseViewModel.NumberofLVI = currentUserReport.NumberofLVI;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CaseViewModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(CaseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CaseExists(int id)
        {
            return _context.Case.Any(e => e.CaseID == id);
        }
    }
}
