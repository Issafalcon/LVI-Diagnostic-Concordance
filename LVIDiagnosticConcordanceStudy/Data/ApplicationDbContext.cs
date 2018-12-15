using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<LVIStudyUser>(ConfigureLVIStudyUser);
            builder.Entity<Case>(ConfigureCase);
            builder.Entity<Report>(ConfigureReport);
        }

        private void ConfigureLVIStudyUser(EntityTypeBuilder<LVIStudyUser> builder)
        {
            builder.ToTable("LVIStudyUser");

            builder.Property(u => u.Gender)
                .HasConversion(new EnumToStringConverter<GenderEnum>());
        }

        private void ConfigureCase(EntityTypeBuilder<Case> builder)
        {
            builder.ToTable("Cases");

            builder.Property(c => c.Id)
                .ValueGeneratedNever();
        }

        private void ConfigureReport(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.OwnsOne(r => r.Statistics, rs =>
            {
                rs.ToTable("ReportStatistics");
            });
        }
    }
}
