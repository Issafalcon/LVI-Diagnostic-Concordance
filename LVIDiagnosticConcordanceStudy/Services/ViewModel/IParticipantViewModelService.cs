using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services.ViewModel
{
    public interface IParticipantViewModelService
    {
        Task<IReadOnlyList<ParticipantViewModel>> GetParticipantListAsync();
        Task<Byte[]> DownloadStudyDataAsync();
    }
}
