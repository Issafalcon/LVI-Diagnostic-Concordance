using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class InterventionData
    {
        public decimal[] TheoreticalYValues { get; set; }
        public int[] ChartXAxis { get; set; }
        public decimal[] ObservedYValues { get; set; }
        public decimal PreTestProbability { get; set; }
        public decimal PostTestProbability { get; set; }
        public bool LVIReported { get; set; } = false;
    }
}
