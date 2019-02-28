using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models.ViewModels
{
    public class ParticipantViewModel
    {
        [Display(Name = "Participant Group")]
        public bool InControlGroup { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        public string Nationality { get; set; }

        [Display(Name = "Work Place (Hospital)")]
        public string PlaceOfWork { get; set; }

        [Display(Name = "Years Qualified")]
        public int YearsQualified { get; set; }

        [Display(Name = "Years In Histopathology")]
        public int YearsInPath { get; set; }

        [Display(Name = "Subspecialty In Breast Pathology")]
        public bool IsBreastSpecialist { get; set; }

        [Display(Name = "Participant Code")]
        public string Code { get; set; }

        [Display(Name = "Completed Study")]
        public bool CompleteStudy { get; set; } = false;
    }
}
