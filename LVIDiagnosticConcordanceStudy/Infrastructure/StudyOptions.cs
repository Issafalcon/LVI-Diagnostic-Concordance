using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Infrastructure
{
    public class StudyOptions
    {
        public StudyOptions()
        {
            // Default Value
            MaximumParticipants = 20;
        }

        public int MaximumParticipants { get; set; }
    }
}
