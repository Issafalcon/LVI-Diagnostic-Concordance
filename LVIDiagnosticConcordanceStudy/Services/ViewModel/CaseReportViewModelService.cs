using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using LVIDiagnosticConcordanceStudy.Services.Domain;

namespace LVIDiagnosticConcordanceStudy.Services.ViewModel
{
    public class CaseReportViewModelService : ICaseReportViewModelService
    {
        private readonly IAsyncRepository<Case> _caseRepository;
        private readonly IRepository<Report> _reportRepository;
        private readonly IReportService _reportService;

        public CaseReportViewModelService(
            IAsyncRepository<Case> caseRepository,
            IRepository<Report> reportRepository,
            IReportService reportService)
        {
            _caseRepository = caseRepository;
            _reportRepository = reportRepository;
            _reportService = reportService;
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

        public async Task CreateCaseReport(CaseReportViewModel caseReport, int caseId, string userId)
        {
            // TODO:
            // 1. Craete new report from the view model
            // 2. Insert the relevant CaseId for the report so they can link
            // 3. Update the isSubmitted field
            // 4. Pass
            Case currentCase = await _caseRepository.GetByIdAsync(caseId);
            await _reportService.CreateReportFromCase(currentCase, caseReport.TumourGrade, caseReport.NumberofLVI, userId);
        }
    }
}
