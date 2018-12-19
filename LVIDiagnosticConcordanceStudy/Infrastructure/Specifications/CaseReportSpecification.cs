using LVIDiagnosticConcordanceStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Infrastructure.Specifications
{
    public class CaseReportSpecification : BaseSpecification<Case>
    {
        public CaseReportSpecification(int caseId, string userId)
            : base(c => c.Id == caseId)
        {
            AddInclude(c => c.Reports.Where(r => r.LVIStudyUserID == userId));
        }
    }
}
