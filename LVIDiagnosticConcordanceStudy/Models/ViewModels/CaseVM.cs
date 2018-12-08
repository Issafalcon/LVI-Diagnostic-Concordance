using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models.ViewModels
{
    public class CaseVM
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
        public int NumberofLVI { get; set; }
    }
}
