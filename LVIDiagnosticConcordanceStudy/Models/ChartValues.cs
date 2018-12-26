using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class ChartValues
    {
        public decimal[] TheoreticalYValues { get; private set; }
        public int[] ChartXAxis { get; private set; }
        public decimal[] ObservedYValues { get; private set; }

        public ChartValues(decimal[] theoreticalYValues, int[] chartXAxis, decimal[] observedYValues)
        {
            TheoreticalYValues = theoreticalYValues;
            ChartXAxis = chartXAxis;
            ObservedYValues = observedYValues;
        }
    }
}
