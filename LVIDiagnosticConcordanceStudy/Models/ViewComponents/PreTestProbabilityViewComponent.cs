using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models.ViewComponents
{
    public class PreTestProbabilityViewComponent : ViewComponent
    {
        public class PreTestProbability
        {
            public decimal PreTest { get; private set; }

            public PreTestProbability(decimal preTestProbability)
            {
                PreTest = preTestProbability;
            }
        }
        

        public IViewComponentResult Invoke(decimal preTestProbability)
        {
            PreTestProbability preTest = new PreTestProbability(preTestProbability);

            return View(preTest);
        }
    }
}
