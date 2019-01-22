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

                //await SeedCasesAsync(serviceProvider);
            }
        }

        private static async Task EnsureAdminUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<LVIStudyUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new LVIStudyUser { UserName = UserName, IsAdmin = true, Email = UserName, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, testUserPw);
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
                new Case(){ Id = 1, PatientAge = 38, TumourSize = 4 },
                new Case(){ Id = 2, PatientAge = 48, TumourSize = 4 },
                new Case(){ Id = 3, PatientAge = 67, TumourSize = 3 },
                new Case(){ Id = 4, PatientAge = 85, TumourSize = 1.5m },
                new Case(){ Id = 5, PatientAge = 45, TumourSize = 3.5m },
                new Case(){ Id = 6, PatientAge = 85, TumourSize = 4 },
                new Case(){ Id = 7, PatientAge = 85, TumourSize = 1.8m },
                new Case(){ Id = 8, PatientAge = 62, TumourSize = 3.2m },
                new Case(){ Id = 9, PatientAge = 55, TumourSize = 2.5m },
                new Case(){ Id = 10, PatientAge = 62, TumourSize = 1 },
                new Case(){ Id = 11, PatientAge = 66, TumourSize = 7.6m },
                new Case(){ Id = 12, PatientAge = 54, TumourSize = 0.7m },
                new Case(){ Id = 13, PatientAge = 48, TumourSize = 1.3m },
                new Case(){ Id = 14, PatientAge = 71, TumourSize = 2.4m },
                new Case(){ Id = 15, PatientAge = 57, TumourSize = 5.5m },
                new Case(){ Id = 16, PatientAge = 78, TumourSize = 3 },
                new Case(){ Id = 17, PatientAge = 73, TumourSize = 0.9m },
                new Case(){ Id = 18, PatientAge = 66, TumourSize = 1.5m },
                new Case(){ Id = 19, PatientAge = 63, TumourSize = 1.5m },
                new Case(){ Id = 20, PatientAge = 61, TumourSize = 0.9m }
            };
        }

    }
}
