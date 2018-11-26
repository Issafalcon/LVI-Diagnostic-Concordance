using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }

        public int UserReportNumber { get; set; }

        [Required]
        [Display(Name = "Grade")]
        public Grade TumourGrade { get; set; }

        [Required]
        [Display(Name = "Number Of LVI")]
        public int NumberofLVI { get; set; }

        // Navigation Properties

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public LVIStudyUser User { get; set; }

        public Case Case { get; set; }

        public ReportStatistics ReportStatistics { get; set; }
    }

    public enum Grade
    {
        G1 = 1,
        G2,
        G3
    }
}
