﻿using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Data.Repository
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public Report GetPreviousReportForUser(string userId)
        {
            var userReports = from report in _dbContext.Report
                              where report.UserID == userId
                              orderby report.UserReportNumber descending
                              select report;

            return userReports.FirstOrDefault();
                              
        }
    }
}