﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using Microsoft.AspNetCore.Identity;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the LVIStudyUser class
    public class LVIStudyUser : IdentityUser
    {
        public bool IsAdmin { get; set; }

        public bool InControlGroup { get; set; }

        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        public string Gender { get; set; }

        [PersonalData]
        public string Nationality { get; set; }

        [PersonalData]
        public string Culture { get; set; }

        [PersonalData]
        public string PlaceOfWork { get; set; }

        [PersonalData]
        public int YearsQualified { get; set; }

        [PersonalData]
        public int YearsInPath { get; set; }

        [PersonalData]
        public bool IsBreastSpecialist { get; set; }

        //CODE_FEATURE
        //public string Code { get; set; }
        //public ParticipantCode ParticipantCode { get; set; }

        public List<Report> Reports { get; set; }

        public bool CompleteStudy { get; set; } = false;
    }
}
