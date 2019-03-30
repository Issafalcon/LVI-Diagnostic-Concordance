using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Services
{
    public interface IUserService
    {
        Task<IReadOnlyList<LVIStudyUser>> GetUserListAsync();
        Task<List<LVIStudyUser>> GetAllUserData();
    }
}
