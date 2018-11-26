using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string initialUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                await EnsureAdminUser(serviceProvider, initialUserPw, "atfiggins@gmail.com");
                await EnsureAdminUser(serviceProvider, initialUserPw, "pablo.pinto1@icloud.com");
            }
        }

        private static async Task EnsureAdminUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<LVIStudyUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new LVIStudyUser { UserName = UserName, IsAdmin = true, Email = UserName };
                var result = await userManager.CreateAsync(user, testUserPw);

            }
        }

    }
}
