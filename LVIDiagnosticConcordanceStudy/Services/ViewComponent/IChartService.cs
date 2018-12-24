using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewComponents;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Services.ViewComponent
{
    public interface IChartService
    {
        Task<ChartValues> GetChartValuesForCaseReport(CaseReportViewModel caseReportViewModel, Report previousReport, int caseId, string userId);
    }
}
