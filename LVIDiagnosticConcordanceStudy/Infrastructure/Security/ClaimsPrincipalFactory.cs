using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Infrastructure.Security
{
    public class ClaimsPrincipalFactory<TUser> : UserClaimsPrincipalFactory<TUser> where TUser : LVIStudyUser
    {
        public ClaimsPrincipalFactory(UserManager<TUser> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            var id = await base.GenerateClaimsAsync(user);
            id.AddClaim(new Claim(CustomClaimTypes.IsAdmin, user.IsAdmin.ToString().ToLower()));
            return id;
        }
    }
}
