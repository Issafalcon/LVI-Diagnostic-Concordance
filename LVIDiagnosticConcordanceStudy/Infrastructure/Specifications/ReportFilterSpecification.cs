﻿using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;

namespace LVIDiagnosticConcordanceStudy.Infrastructure.Specifications
{
    public class ReportFilterSpecification : BaseSpecification<Report>
    {
        public ReportFilterSpecification(string userId, int? caseId, bool orderByReportNumberDesc = false, bool includeCase = false)
            : base(r => (string.IsNullOrEmpty(userId) || r.LVIStudyUserID == userId) && 
            (!caseId.HasValue || r.CaseId == caseId))
        {
            if (includeCase)
            {
                AddInclude(r => r.Case);
            }

            if (orderByReportNumberDesc)
            {
                ApplyOrderByDescending(r => r.UserReportNumber);
            }
        }
    }
}
