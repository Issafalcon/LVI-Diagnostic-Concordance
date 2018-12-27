using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class InterventionData
    {
        public decimal[] TheoreticalYValues { get; private set; }
        public int[] ChartXAxis { get; private set; }
        public decimal[] ObservedYValues { get; private set; }
        public decimal PreTestProbability { get; private set; }
        public decimal PostTestProbability { get; private set; }

        public InterventionData(decimal[] theoreticalYValues, int[] chartXAxis, decimal[] observedYValues, decimal preTestProbability, decimal postTestProbability)
        {
            TheoreticalYValues = theoreticalYValues;
            ChartXAxis = chartXAxis;
            ObservedYValues = observedYValues;
            PreTestProbability = preTestProbability;
            PostTestProbability = postTestProbability;
        }
    }
}
