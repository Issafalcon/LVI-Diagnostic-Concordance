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
        public DbSet<ParticipantCode> ParticipantCode { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LVIStudyUser>(ConfigureLVIStudyUser);
            builder.Entity<Case>(ConfigureCase);
            builder.Entity<Report>(ConfigureReport);
        }

        private void ConfigureLVIStudyUser(EntityTypeBuilder<LVIStudyUser> builder)
        {
            builder.ToTable("LVIStudyUser");

            builder.Property(u => u.Gender)
                .HasConversion(new EnumToStringConverter<GenderEnum>());

            builder.HasMany(u => u.Reports)
                .WithOne(r => r.LVIStudyUser)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.ParticipantCode)
                .WithOne(pc => pc.LVIStudyUser)
                .HasForeignKey<LVIStudyUser>(u => u.Code)
                .HasPrincipalKey<ParticipantCode>(pc => pc.Code);
        }

        private void ConfigureCase(EntityTypeBuilder<Case> builder)
        {
            builder.ToTable("Cases");
            builder.HasIndex(c => c.CaseNumber)
                .IsUnique();
        }

        private void ConfigureReport(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.OwnsOne(r => r.Statistics, rs =>
            {
                rs.ToTable("ReportStatistics");
            });
        }

        private void ConfigureCase(EntityTypeBuilder<ParticipantCode> builder)
        {
            builder.ToTable("ParticipantCodes");

            //builder.HasOne(pc => pc.LVIStudyUser)
            //    .WithOne(su => su.ParticipantCode)
            //    .HasPrincipalKey<ParticipantCode>(pc => pc.Code)
            //    .HasForeignKey<LVIStudyUser>(su => su.Code);
        }
    }
}
