using LVIDiagnosticConcordanceStudy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class Case : BaseEntity
    {
        public int PatientAge { get; set; }

        public decimal TumourSize { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
