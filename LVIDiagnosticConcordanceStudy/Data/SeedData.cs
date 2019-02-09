using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Models;
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

                await SeedCasesAsync(serviceProvider);
            }
        }

        private static async Task EnsureAdminUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<LVIStudyUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            var result = new IdentityResult();
            if (user == null)
            {
                user = new LVIStudyUser { UserName = UserName, IsAdmin = true, Email = UserName, EmailConfirmed = true };
                await userManager.CreateAsync(user, testUserPw);
            }
        }

        private static async Task SeedCasesAsync(IServiceProvider serviceProvider)
        {
            var applicationContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!applicationContext.Case.Any())
            {
                applicationContext.Case.AddRange(
                    GetPreconfiguredCases());

                await applicationContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Case> GetPreconfiguredCases()
        {
            return new List<Case>()
            {
                new Case(){ PatientAge = 38, TumourSize = 4 },
                new Case(){ PatientAge = 48, TumourSize = 4 },
                new Case(){ PatientAge = 67, TumourSize = 3 },
                new Case(){ PatientAge = 85, TumourSize = 1.5m },
                new Case(){ PatientAge = 45, TumourSize = 3.5m },
                new Case(){ PatientAge = 85, TumourSize = 4 },
                new Case(){ PatientAge = 85, TumourSize = 1.8m },
                new Case(){ PatientAge = 62, TumourSize = 3.2m },
                new Case(){ PatientAge = 55, TumourSize = 2.5m },
                new Case(){ PatientAge = 62, TumourSize = 1 },
                new Case(){ PatientAge = 66, TumourSize = 7.6m },
                new Case(){ PatientAge = 54, TumourSize = 0.7m },
                new Case(){ PatientAge = 48, TumourSize = 1.3m },
                new Case(){ PatientAge = 71, TumourSize = 2.4m },
                new Case(){ PatientAge = 57, TumourSize = 5.5m },
                new Case(){ PatientAge = 78, TumourSize = 3 },
                new Case(){ PatientAge = 73, TumourSize = 0.9m },
                new Case(){ PatientAge = 66, TumourSize = 1.5m },
                new Case(){ PatientAge = 63, TumourSize = 1.5m },
                new Case(){ PatientAge = 61, TumourSize = 0.9m }
            };
        }

    }
}
