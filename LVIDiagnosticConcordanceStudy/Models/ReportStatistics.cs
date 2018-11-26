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
        public int ReportStatisticsID { get; set; }

        public bool LVIPresent { get; set; }
        [Column(TypeName = "float")]
        public decimal ProbLVIPos50Plus { get; set; }
        [Column(TypeName = "float")]
        public decimal ProbLVINeg50Plus { get; set; }
        [Column(TypeName = "float")]
        public decimal BayesForAge { get; set; }
        [Column(TypeName = "float")]
        public decimal ProbLVIPosSize { get; set; }
        [Column(TypeName = "float")]
        public decimal ProbLVINegSize { get; set; }
        [Column(TypeName = "float")]
        public decimal BayesForSize { get; set; }
        [Column(TypeName = "float")]
        public decimal ProbLVIPosGrade { get; set; }
        [Column(TypeName = "float")]
        public decimal ProbLVINegGrade { get; set; }
        [Column(TypeName = "float")]
        public decimal BayesForGrade { get; set; }
        [Column(TypeName = "float")]
        public decimal ProbLVIPosNumberOfLVI { get; set; }
        [Column(TypeName = "float")]
        public decimal ProbLVINegNumberOfLVI { get; set; }
        [Column(TypeName = "float")]
        public decimal BayesForNumberOfLVI { get; set; }

        [Column(TypeName = "float")]
        public decimal CumulativeBayesForSize { get; set; }
        [Column(TypeName = "float")]
        public decimal CumulativeAverageBayesForSize { get; set; }

        public int CumulativeCasesWithLVIPos { get; set; }

        [Column(TypeName = "float")]
        public decimal BinomialDist { get; set; }
        [Column(TypeName = "float")]
        public double TheoreticalBinomialDist { get; set; }

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
            BayesForSize = CalculateBayes(BayesForAge, ProbLVIPosSize, ProbLVINegSize, 1 - BayesForAge);

            ProbLVIPosGrade = (int)grade.Value == 1 ? DataConstants.GradeOneLVIPos
                : (int)grade.Value == 2 ? DataConstants.GradeTwoLVIPos
                : DataConstants.GradeThreeLVIPos;
            ProbLVINegGrade = (int)grade.Value == 1 ? DataConstants.GradeOneLVINeg
                : (int)grade.Value == 2 ? DataConstants.GradeTwoLVINeg
                : DataConstants.GradeThreeLVINeg;
            BayesForGrade = CalculateBayes(BayesForSize, ProbLVIPosGrade, ProbLVINegGrade, 1 - BayesForSize);

            ProbLVIPosNumberOfLVI = numLVISeen == 0 ? DataConstants.ZeroLVIImagesLVIPos
                : numLVISeen < 3 ? DataConstants.OneToTwoLVIImagesLVIPos
                : numLVISeen < 4 ? DataConstants.TwoToThreeLVIImagesLVIPos
                : DataConstants.FivePlusLVIImagesLVIPos;
            ProbLVINegNumberOfLVI = numLVISeen == 0 ? DataConstants.ZeroLVIImagesLVINeg
                : numLVISeen < 3 ? DataConstants.OneToTwoLVIImagesLVINeg
                : numLVISeen < 4 ? DataConstants.TwoToThreeLVIImagesLVINeg
                : DataConstants.FivePlusLVIImagesLVINeg;
            BayesForNumberOfLVI = CalculateBayes(BayesForGrade, ProbLVIPosNumberOfLVI, ProbLVINegNumberOfLVI, 1 - BayesForGrade);
        }

        private decimal CalculateBayes(decimal baseProb, decimal positivePredictiveProb, decimal negativePredictiveProb, decimal baseNegativePredictiveProb)
        {
            decimal posTimesBase = positivePredictiveProb * baseProb;

            return (posTimesBase) / (posTimesBase + (negativePredictiveProb * baseNegativePredictiveProb));
        }
    }
}
