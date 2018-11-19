using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the LVIStudyUser class
    public class LVIStudyUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public bool InControlGroup { get; set; }
        public string Culture { get; set; }
    }
}
