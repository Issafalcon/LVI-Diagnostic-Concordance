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
        public decimal CumulativeBayesForGrade { get; set; }
        [Column(TypeName = "float")]
        public decimal CumulativeAverageBayesForGrade { get; set; }

        public int CumulativeCasesWithLVIPos { get; set; }

        [Column(TypeName = "float")]
        public decimal BinomialDist { get; set; }

    }
}
