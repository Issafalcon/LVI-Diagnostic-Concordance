using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Services;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;

namespace LVIDiagnosticConcordanceStudy.Services.ViewModel
{
    public class ParticipantViewModelService : IParticipantViewModelService
    {
        private readonly IUserService _userService;
        private readonly IExcelWriter _excelWriter;

        public ParticipantViewModelService(IUserService userService, IExcelWriter excelWriter)
        {
            _userService = userService;
            _excelWriter = excelWriter;
        }

        public async Task<IReadOnlyList<ParticipantViewModel>> GetParticipantListAsync()
        {
            var userList = await _userService.GetUserListAsync();

            List<ParticipantViewModel> participants = new List<ParticipantViewModel>();

            foreach (var user in userList)
            {
                ParticipantViewModel participant = new ParticipantViewModel
                {
                    FullName = user.FirstName + ' ' + user.LastName,
                    Nationality = user.Nationality,
                    PlaceOfWork = user.PlaceOfWork,
                    YearsQualified = user.YearsQualified,
                    YearsInPath = user.YearsInPath,
                    IsBreastSpecialist = user.IsBreastSpecialist,
                    InControlGroup = user.InControlGroup,
                    //Code = user.Code,
                    CompleteStudy = user.CompleteStudy
                };

                participants.Add(participant);
            }

            return participants;
        }

        public async Task<Byte[]> DownloadStudyDataAsync()
        {
            var data = await _userService.GetAllUserData();

            return _excelWriter.WriteToExcel<LVIStudyUser>(data);
        }
    }
}
