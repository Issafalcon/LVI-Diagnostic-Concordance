﻿// <auto-generated />
using System;
using LVIDiagnosticConcordanceStudy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LVIDiagnosticConcordanceStudy.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190228175609_adding case number")]
    partial class addingcasenumber
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.LVIStudyUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Code");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Culture");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<bool>("InControlGroup");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsBreastSpecialist");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nationality");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PlaceOfWork");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("YearsInPath");

                    b.Property<int>("YearsQualified");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("LVIStudyUser");
                });

            modelBuilder.Entity("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.ParticipantCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<bool>("IsUsed");

                    b.HasKey("Id");

                    b.ToTable("ParticipantCode");
                });

            modelBuilder.Entity("LVIDiagnosticConcordanceStudy.Models.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseNumber");

                    b.Property<int>("PatientAge");

                    b.Property<string>("SlideURL");

                    b.Property<decimal>("TumourSize");

                    b.HasKey("Id");

                    b.HasIndex("CaseNumber")
                        .IsUnique();

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaseId");

                    b.Property<bool>("IsSubmitted");

                    b.Property<string>("LVIStudyUserID");

                    b.Property<int>("NumberofLVI");

                    b.Property<int>("TumourGrade");

                    b.Property<int>("UserReportNumber");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.HasIndex("LVIStudyUserID");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.LVIStudyUser", b =>
                {
                    b.HasOne("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.ParticipantCode", "ParticipantCode")
                        .WithOne("LVIStudyUser")
                        .HasForeignKey("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.LVIStudyUser", "Code")
                        .HasPrincipalKey("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.ParticipantCode", "Code");
                });

            modelBuilder.Entity("LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate.Report", b =>
                {
                    b.HasOne("LVIDiagnosticConcordanceStudy.Models.Case", "Case")
                        .WithMany("Reports")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.LVIStudyUser", "LVIStudyUser")
                        .WithMany("Reports")
                        .HasForeignKey("LVIStudyUserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("LVIDiagnosticConcordanceStudy.Models.ReportStatistics", "Statistics", b1 =>
                        {
                            b1.Property<int>("ReportId");

                            b1.Property<double>("BayesForAge")
                                .HasColumnType("float");

                            b1.Property<double>("BayesForGrade")
                                .HasColumnType("float");

                            b1.Property<double>("BayesForNumberOfLVI")
                                .HasColumnType("float");

                            b1.Property<double>("BayesForSize")
                                .HasColumnType("float");

                            b1.Property<double>("BinomialDist")
                                .HasColumnType("float");

                            b1.Property<double>("CumulativeAverageBayesForGrade")
                                .HasColumnType("float");

                            b1.Property<double>("CumulativeBayesForGrade")
                                .HasColumnType("float");

                            b1.Property<int>("CumulativeCasesWithLVIPos");

                            b1.Property<bool>("LVIPresent");

                            b1.Property<double>("ProbLVINeg50Plus")
                                .HasColumnType("float");

                            b1.Property<double>("ProbLVINegGrade")
                                .HasColumnType("float");

                            b1.Property<double>("ProbLVINegNumberOfLVI")
                                .HasColumnType("float");

                            b1.Property<double>("ProbLVINegSize")
                                .HasColumnType("float");

                            b1.Property<double>("ProbLVIPos50Plus")
                                .HasColumnType("float");

                            b1.Property<double>("ProbLVIPosGrade")
                                .HasColumnType("float");

                            b1.Property<double>("ProbLVIPosNumberOfLVI")
                                .HasColumnType("float");

                            b1.Property<double>("ProbLVIPosSize")
                                .HasColumnType("float");

                            b1.HasKey("ReportId");

                            b1.ToTable("ReportStatistics");

                            b1.HasOne("LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate.Report")
                                .WithOne("Statistics")
                                .HasForeignKey("LVIDiagnosticConcordanceStudy.Models.ReportStatistics", "ReportId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.LVIStudyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.LVIStudyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.LVIStudyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LVIDiagnosticConcordanceStudy.Areas.Identity.Data.LVIStudyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
