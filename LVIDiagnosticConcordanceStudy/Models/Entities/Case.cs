using LVIDiagnosticConcordanceStudy.Models.Entities;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System.Collections.Generic;

namespace LVIDiagnosticConcordanceStudy.Models
{
    public class Case : BaseEntity
    {
        public int PatientAge { get; set; }

        public decimal TumourSize { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
