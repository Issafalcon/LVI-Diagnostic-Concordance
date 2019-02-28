using System.Collections.Generic;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Data;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace LVIDiagnosticConcordanceStudy.Services
{
    public class CaseService : ICaseService
    {
        private IAsyncRepository<Case> _asyncCaseRepository;
        private IRepository<Case> _caseRepository;
        protected readonly ApplicationDbContext _dbContext;

        public CaseService(IAsyncRepository<Case> asyncCaseRepository, IRepository<Case> caseRepository, ApplicationDbContext dbContext)
        {
            _asyncCaseRepository = asyncCaseRepository;
            _caseRepository = caseRepository;
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Case>> GetOrderedCasesAsync(bool includeReports = false)
        {
            return await _asyncCaseRepository.ListAsync(new CaseFilterSpecification(null, includeReports: includeReports, orderByCaseNumber: true));
        }

        public async Task<Case> GetCaseByIdAsync(int id)
        {
            return await _asyncCaseRepository.GetByIdAsync(id);
        }

        public async Task ResetCasesAsync()
        {
            var cases = _caseRepository.ListAll();
            await _asyncCaseRepository.DeleteRangeAsync(cases);
            _dbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT('cases', RESEED, 0)");
        }
    }
}
