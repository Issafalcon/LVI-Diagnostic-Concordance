using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;

namespace LVIDiagnosticConcordanceStudy.Infrastructure.Specifications
{
    public class ReportFilterSpecification : BaseSpecification<Report>
    {
        public ReportFilterSpecification(string userId, int? caseId, bool orderByReportNumberDesc = false)
            : base(r => (string.IsNullOrEmpty(userId) || r.UserID == userId) && 
            (!caseId.HasValue || r.CaseId == caseId))
        {
            if (orderByReportNumberDesc)
            {
                ApplyOrderByDescending(r => r.UserReportNumber);
            }
        }
    }
}
