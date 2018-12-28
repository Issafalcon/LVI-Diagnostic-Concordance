using LVIDiagnosticConcordanceStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Infrastructure.Specifications
{
    public class CaseFilterSpecification : BaseSpecification<Case>
    {
        public CaseFilterSpecification(int? caseId, bool includeReports = false)
            : base(c => (!caseId.HasValue || c.Id == caseId))
        {
            if (includeReports)
            {
                AddInclude(c => c.Reports);
            }
        }
    }
}
