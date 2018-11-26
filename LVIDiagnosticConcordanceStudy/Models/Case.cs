using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TumourSize { get; set; }

        // Navigation Properties
        ICollection<Report> Reports { get; set; }
    }
}
