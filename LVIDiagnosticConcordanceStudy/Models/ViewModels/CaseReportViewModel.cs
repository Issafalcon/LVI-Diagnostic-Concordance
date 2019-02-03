using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace LVIDiagnosticConcordanceStudy.Models.ViewModels
{
    public class CaseReportViewModel
    {
        [Display(Name = "Age")]
        public int PatientAge { get; set; }

        [Display(Name = "Tumour Size in cm")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public decimal TumourSize { get; set; }

        [Required]
        [Display(Name = "Grade")]
        public Grade TumourGrade { get; set; }

        [Required]
        [Display(Name = "Number Of LVI")]
        [RegularExpression(@"^\d+$", ErrorMessage = "The Number Of LVI must be an integer of 0 or greater")]
        public int NumberofLVI { get; set; }

        public bool IsSubmitted { get; set; }
    }
}
