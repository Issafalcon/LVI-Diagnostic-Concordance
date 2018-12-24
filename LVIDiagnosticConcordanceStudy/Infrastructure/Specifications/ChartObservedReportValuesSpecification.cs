using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Infrastructure.Specifications
{
    public class ChartObservedReportValuesSpecification : BaseSpecification<Report>
    {
        public ChartObservedReportValuesSpecification(int? cumulativeCasesLVIPosCount)
           : base(r => (!cumulativeCasesLVIPosCount.HasValue || r.UserReportNumber - 1 == cumulativeCasesLVIPosCount))
        {

        }
    }
}
