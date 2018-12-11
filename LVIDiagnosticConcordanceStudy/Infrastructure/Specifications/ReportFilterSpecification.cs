using LVIDiagnosticConcordanceStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Infrastructure.Specifications
{
    public class ReportFilterSpecification : BaseSpecification<Report>
    {
        public ReportFilterSpecification(string userId, int? caseId = null, bool orderByReportNumberDesc = false)
            : base(r => (!string.IsNullOrEmpty(userId) || r.UserID == userId) && 
            (!caseId.HasValue || r.CaseId == caseId))
        {
            if (orderByReportNumberDesc)
            {
                ApplyOrderByDescending(r => r.UserReportNumber);
            }
        }
    }
}
