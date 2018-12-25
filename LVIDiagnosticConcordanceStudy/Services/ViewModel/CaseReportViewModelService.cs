
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Models;
using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using LVIDiagnosticConcordanceStudy.Services.Domain;

namespace LVIDiagnosticConcordanceStudy.Services.ViewModel
{
    public class CaseReportViewModelService : ICaseReportViewModelService
    {
        private readonly IAsyncRepository<Case> _caseRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IReportService _reportService;

        public CaseReportViewModelService(
            IAsyncRepository<Case> caseRepository,
            IReportRepository reportRepository,
            IReportService reportService)
        {
            _caseRepository = caseRepository;
            _reportRepository = reportRepository;
            _reportService = reportService;
        }

        public async Task<CaseReportViewModel> GetCaseReportForUser(string userId, int caseId)
        {
            
            Case currentCase = await _caseRepository.GetByIdAsync(caseId);

            Report report = GetExistingReport(userId, caseId);

            if (currentCase == null)
            {
                return null;
            }

            CaseReportViewModel caseReport = new CaseReportViewModel
            {
                PatientAge = currentCase.PatientAge,
                TumourSize = currentCase.TumourSize,
                IsSubmitted = false
            };

            if (report != null)
            {
                caseReport.TumourGrade = report.TumourGrade;
                caseReport.NumberofLVI = report.NumberofLVI;
                caseReport.IsSubmitted = report.IsSubmitted;
            }

            return caseReport;
        }

        public Report GetExistingReport (string userId, int caseId)
        {
            var reportFilter = new ReportFilterSpecification(userId, caseId);
            return _reportRepository.GetSingleBySpec(reportFilter);
        }

        public async Task CreateOrUpdateCaseReport(CaseReportViewModel caseReport, Report existingReport, int caseId, string userId, bool isSubmitted = false)
        {
            Case currentCase = await _caseRepository.GetByIdAsync(caseId);
            await _reportService.CreateOrUpdateReportFromCase(currentCase, existingReport, caseReport.TumourGrade, caseReport.NumberofLVI, userId, isSubmitted);
        }

        public async Task<int> GetCaseCount()
        {
            var allCasesSpecification = new CaseFilterSpecification(null);
            return await _caseRepository.CountAsync(allCasesSpecification);
        }

        public int[] GetSubmittedCaseReportIds(string userId)
        {
            return _reportRepository.GetSubmittedReportIdsForUser(userId);
        }
    }
}
