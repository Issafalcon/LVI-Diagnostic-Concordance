using LVIDiagnosticConcordanceStudy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Data
{
    public class ParticipantCode : BaseEntity
    {
        [Required]
        public string Code { get; set; }

        public bool IsUsed { get; set; }

        public LVIStudyUser LVIStudyUser { get; set; }
    }
}
