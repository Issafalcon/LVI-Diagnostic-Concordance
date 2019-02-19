using Microsoft.AspNetCore.Mvc;

namespace LVIDiagnosticConcordanceStudy.Models.ViewComponents
{
    public class AdditionalProbabilityViewComponent : ViewComponent
    {
        public class Probabilities
        {
            public decimal PreTest { get; private set; }
            public decimal PostTest { get; private set; }
            public decimal Observed { get; private set; }
            public int ConcordanceCriteriaMet { get; private set; }
            public bool LVIReported { get; private set; }

            public Probabilities(decimal preTestProbability, decimal postTestProbability, decimal observed, bool lviReported)
            {
                PreTest = preTestProbability;
                PostTest = postTestProbability;
                Observed = observed;
                LVIReported = lviReported;
            }

            public void CalculateConcordance()
            {
                int criteriaMet = 0;
                criteriaMet += this.PreTest < DataConstants.FiftyPercent && LVIReported ? 0 : 1;
                criteriaMet += this.PostTest < DataConstants.FiftyPercent && LVIReported ? 0 : 1;
                criteriaMet += this.Observed < 0.2m && LVIReported ? 0 : 1;

                ConcordanceCriteriaMet = criteriaMet;
            }
        }

        public IViewComponentResult Invoke(decimal preTestProbability, decimal postTestProbability, decimal observed, bool lviReported)
        {
            Probabilities probabilityData = new Probabilities(preTestProbability, postTestProbability, observed, lviReported);
            probabilityData.CalculateConcordance();

            return View(probabilityData);
        }
    }

}
