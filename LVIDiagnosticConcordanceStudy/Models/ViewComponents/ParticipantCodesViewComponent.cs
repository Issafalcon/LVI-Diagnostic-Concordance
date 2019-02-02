using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models.ViewComponents
{
    public class ParticipantCodesViewComponent : ViewComponent
    {
        private IAsyncRepository<ParticipantCode> _participantCodeRepository;

        public ParticipantCodesViewComponent(
            IAsyncRepository<ParticipantCode> participantCodeRepository)
        {
            _participantCodeRepository = participantCodeRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IReadOnlyList<ParticipantCode> codes = await _participantCodeRepository.ListAllAsync();

            return View(codes);
        }
    }
}
