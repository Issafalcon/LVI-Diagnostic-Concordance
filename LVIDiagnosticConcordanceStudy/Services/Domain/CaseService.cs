using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models;

namespace LVIDiagnosticConcordanceStudy.Services
{
    public class CaseService : ICaseService
    {
        private IAsyncRepository<Case> _caseRepository;

        public CaseService(IAsyncRepository<Case> caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public async Task<IReadOnlyList<Case>> GetOrderedCasesAsync()
        {
            return await _caseRepository.ListAsync(new CaseFilterSpecification(null, orderByCaseNumber: true));
        }

        public async Task<Case> GetCaseByIdAsync(int id)
        {
            return await _caseRepository.GetByIdAsync(id);
        }
    }
}
