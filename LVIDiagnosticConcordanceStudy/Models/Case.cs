using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class Case
    {
        [Key]
        public int CaseID { get; set; }

        [Display(Name = "Age")]
        public int PatientAge { get; set; }

        [Display(Name = "Tumour Size in cm")]
        public decimal TumourSize { get; set; }
    }
}
