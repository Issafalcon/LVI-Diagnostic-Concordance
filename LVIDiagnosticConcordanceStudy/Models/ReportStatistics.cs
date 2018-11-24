using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class ReportStatistics
    {
        [Key]
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
    }
}
