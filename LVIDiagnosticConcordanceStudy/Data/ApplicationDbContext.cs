using System;
using System.Collections.Generic;
using System.Text;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LVIDiagnosticConcordanceStudy.Data
{
    public class ApplicationDbContext : IdentityDbContext<LVIStudyUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Case> Case { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportStatistics> ReportStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<LVIStudyUser>()
                .Property(e => e.Gender)
                .HasConversion(new EnumToStringConverter<GenderEnum>());
        }
    }
}
