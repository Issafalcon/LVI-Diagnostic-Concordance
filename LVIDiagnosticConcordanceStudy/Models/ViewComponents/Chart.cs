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

        public Chart(IChartService chartService)
        {
            _chartService = chartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
