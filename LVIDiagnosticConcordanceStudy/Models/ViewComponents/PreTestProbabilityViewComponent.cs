using Microsoft.AspNetCore.Mvc;

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
