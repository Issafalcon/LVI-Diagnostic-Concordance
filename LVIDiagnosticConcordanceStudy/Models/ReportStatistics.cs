using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class ReportStatistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public bool LVIPresent { get; set; }
        public decimal ProbLVIPos50Plus { get; set; }
        public decimal ProbLVINeg50Plus { get; set; }
        public decimal BayesForAge { get; set; }
        public decimal ProbLVIPosSize { get; set; }
        public decimal ProbLVINegSize { get; set; }
        public decimal BayesForSize { get; set; }
        public decimal ProbLVIPosGrade { get; set; }
        public decimal ProbLVINegGrade { get; set; }
        public decimal BayesForGrade { get; set; }
        public decimal ProbLVIPosNumberOfLVI { get; set; }
        public decimal ProbLVINegNumberOfLVI { get; set; }
        public decimal BayesForNumberOfLVI { get; set; }

        public decimal CumulativeBayesForSize { get; set; }
        public decimal CumulativeAverageBayesForSize { get; set; }
        public decimal CumulativeCasesWithLVIPos { get; set; }

        public decimal BinomialDist { get; set; }
        public decimal TheoreticalBinomialDist { get; set; }

        public bool IsSubmitted { get; set; }

        // Navigation Properties
        public int ReportID { get; set; }
        public Report Report { get; set; }

        public void CalculateStatistics()
        {
            int? ptAge = Report?.Case?.PatientAge;
            decimal? tumourSize = Report?.Case?.TumourSize;
            Grade? grade = Report?.TumourGrade;
            int? numLVISeen = Report?.NumberofLVI;

            if (!ptAge.HasValue || !tumourSize.HasValue || !grade.HasValue || !numLVISeen.HasValue)
            {
                throw new System.ArgumentException("Calculate Statistics not performed. Required case report information is missing");
            }

            ProbLVIPos50Plus = ptAge < 51 ? DataConstants.BelowFiftyYearsLVIPos : 1 - DataConstants.BelowFiftyYearsLVIPos;
            ProbLVINeg50Plus = ptAge < 51 ? DataConstants.BelowFiftyYearsLVINeg : 1 - DataConstants.BelowFiftyYearsLVINeg;
            BayesForAge = CalculateBayes(DataConstants.ProbLVIPos,
                DataConstants.BelowFiftyYearsLVIPos,
                DataConstants.BelowFiftyYearsLVINeg,
                DataConstants.ProbLVINeg);

            ProbLVIPosSize = tumourSize > 2 ? DataConstants.TwoToFivecmLVIPos
                : tumourSize > 1 ? DataConstants.OneToTwocmLVIPos
                : DataConstants.BelowOnecmLVIPos;

            ProbLVINegSize = tumourSize > 2 ? DataConstants.TwoToFivecmLVINeg
                : tumourSize > 1 ? DataConstants.OneToTwocmLVINeg
                : DataConstants.BelowOnecmLVINeg;
                

        }

        private decimal CalculateBayes(decimal baseProb, decimal positivePredictiveProb, decimal negativePredictiveProb, decimal baseNegativePredictiveProb)
        {
            decimal posTimesBase = positivePredictiveProb * baseProb;

            return (posTimesBase) / (posTimesBase + (negativePredictiveProb * baseNegativePredictiveProb));
        }
    }
}
