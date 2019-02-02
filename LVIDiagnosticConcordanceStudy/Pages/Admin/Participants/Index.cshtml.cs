using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Services;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using LVIDiagnosticConcordanceStudy.Services.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LVIDiagnosticConcordanceStudy.Pages.Admin.Participants
{
    public class IndexModel : PageModel
    {
        private readonly IParticipantViewModelService _participantService;
        private readonly IAsyncRepository<ParticipantCode> _participantCodeRepository;

        public IndexModel(
            IParticipantViewModelService participantService,
            IAsyncRepository<ParticipantCode> participantCodeRepository)
        {
            _participantService = participantService;
            _participantCodeRepository = participantCodeRepository;
        }

        public IReadOnlyList<ParticipantViewModel> Participants { get; set; }

        public async Task OnGetAsync()
        {
            Participants = await _participantService.GetParticipantListAsync();
        }

        public async Task<IActionResult> OnPostGenerateParticipantCodeAsync()
        {
            string newCode = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            IReadOnlyList<ParticipantCode> existingParticipantCodes = await _participantCodeRepository.ListAllAsync();

            var existingCodes = from code in existingParticipantCodes
                                select code.Code;

            while (existingCodes.Contains(newCode))
            {
                newCode = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            }

            ParticipantCode newParticipantCode = new ParticipantCode
            {
                Code = newCode,
                IsUsed = false
            };

            await _participantCodeRepository.AddAsync(newParticipantCode);

            return RedirectToPage("Index");
        }
    }
}