using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;

namespace LVIDiagnosticConcordanceStudy.Services.ViewModel
{
    public class CaseReportViewModelService : ICaseReportViewModelService
    {
        private readonly IAsyncRepository<Case> _caseRepository;
        private readonly IRepository<Report> _reportRepository;

        public CaseReportViewModelService(
            IAsyncRepository<Case> caseRepository,
            IRepository<Report> reportRepository)
        {
            _caseRepository = caseRepository;
            _reportRepository = reportRepository;
        }

        public async Task<CaseReportViewModel> GetCaseReportForUser(string userId, int caseId)
        {
            var reportFilter = new ReportFilterSpecification(userId, caseId);
            Case currentCase = await _caseRepository.GetByIdAsync(caseId);

            Report report = _reportRepository.GetSingleBySpec(reportFilter);

            if (currentCase == null)
            {
                return null;
            }

            CaseReportViewModel caseReport = new CaseReportViewModel
            {
                PatientAge = currentCase.PatientAge,
                TumourSize = currentCase.TumourSize
            };

            if (report != null)
            {
                caseReport.TumourGrade = report.TumourGrade;
                caseReport.NumberofLVI = report.NumberofLVI;
            }

            return caseReport;
        }

        public async Task CreateCaseReport(CaseReportViewModel caseReport)
        {
            // TODO:
            // 1. Craete new report
        }
    }
}
