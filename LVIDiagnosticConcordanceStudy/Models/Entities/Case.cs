﻿using LVIDiagnosticConcordanceStudy.Models.Entities;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class Case : BaseEntity
    {
        [Display(Name = "Case Number")]
        public int CaseNumber { get; set; }

        [Display(Name = "Age")]
        public int PatientAge { get; set; }

        [Display(Name = "Tumour Size in cm")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F2}")]
        public decimal TumourSize { get; set; }

        [Display(Name = "Case Link")]
        [DataType(DataType.Url)]
        public string SlideURL { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
