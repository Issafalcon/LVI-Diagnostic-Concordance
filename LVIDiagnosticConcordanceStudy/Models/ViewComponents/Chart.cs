using LVIDiagnosticConcordanceStudy.Models.Entities.ReportAggregate;
using LVIDiagnosticConcordanceStudy.Models.ViewModels;
using LVIDiagnosticConcordanceStudy.Services.Domain;
using LVIDiagnosticConcordanceStudy.Services.ViewComponent;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models.ViewComponents
{
    public class Chart : ViewComponent
    {
        private readonly IChartService _chartService;
        private readonly IReportService _reportService;

        public Chart(IChartService chartService, IReportService reportService)
        {
            _chartService = chartService;
            _reportService = reportService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CaseReportViewModel caseReportViewModel, int caseId, string userId)
        {
            Report previousReport = _reportService.GetPreviousUserReport(userId);
            ChartValues chartValues = await _chartService.GetChartValuesForCaseReport(caseReportViewModel, previousReport, caseId, userId);
            return View(chartValues);
        }

    }

    public class ChartValues
    {
        public decimal[] TheoreticalSeriesYAxis { get; private set; }
        public int[] ChartXAxis { get; private set; }
        public decimal ObservedYValue { get; private set; }
        public int ObservedXValue { get; private set; }

        public ChartValues(decimal[] theoreticalSeriesYAxis, int[] chartXAxis, decimal observedYValue, int observedXValue)
        {
            TheoreticalSeriesYAxis = theoreticalSeriesYAxis;
            ChartXAxis = chartXAxis;
            ObservedYValue = observedYValue;
            ObservedXValue = observedXValue;
        }
    }
}
