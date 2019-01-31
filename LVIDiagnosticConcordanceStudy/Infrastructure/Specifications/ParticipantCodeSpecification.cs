using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Infrastructure.Specifications
{
    public class ParticipantCodeSpecification : BaseSpecification<ParticipantCode>
    {
        public ParticipantCodeSpecification(bool? isUsed)
            : base (pc => (!isUsed.HasValue || pc.IsUsed == isUsed))
        {

        }
    }
}
