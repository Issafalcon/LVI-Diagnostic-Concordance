using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate
{
    public class Report : BaseEntity
    {
        public int UserReportNumber { get; private set; }
        public ReportStatistics Statistics { get; internal set; }
        public string LVIStudyUserID { get; private set; }
        public int CaseId { get; private set; }

        public Report()
        {
            // Required by EF
        }

        public Report(int userReportNumber, string userId, int caseId, ReportStatistics statistics, bool isSubmitted = false)
        {
            UserReportNumber = userReportNumber;
            LVIStudyUserID = userId;
            CaseId = caseId;
            Statistics = statistics;
            IsSubmitted = isSubmitted;
        }

        public Grade TumourGrade { get; set; }
        public int NumberofLVI { get; set; }
        public bool IsSubmitted { get; set; }

        public LVIStudyUser LVIStudyUser { get; set; }
        public Case Case { get; set; }
    }

    public enum Grade
    {
        G1 = 1,
        G2,
        G3
    }
}
