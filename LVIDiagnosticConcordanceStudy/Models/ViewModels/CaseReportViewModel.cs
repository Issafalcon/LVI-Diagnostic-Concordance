using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Localization;

namespace LVIDiagnosticConcordanceStudy.Models.ViewModels
{
    public class CaseReportViewModel
    {
        [Display(Name = "Age")]
        public int PatientAge { get; set; }

        [Display(Name = "Tumour Size in cm")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:F2}")]
        public decimal TumourSize { get; set; }

        [Required(ErrorMessage = "Required_Field_Error")]
        [Display(Name = "Grade")]
        public Grade TumourGrade { get; set; }

        [Required(ErrorMessage = "Required_Field_Error")]
        [Display(Name = "Number Of LVI")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Num_LVI_Error")]
        public int NumberofLVI { get; set; }

        [Display(Name = "Case Link")]
        [DataType(DataType.Url)]
        public string SlideUrl { get; set; }

        public bool IsSubmitted { get; set; }
    }
}
