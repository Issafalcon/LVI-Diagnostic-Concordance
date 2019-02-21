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

                if (LVIReported)
                {
                    if (Observed > 0.2m)
                    {
                        criteriaMet = PreTest >= DataConstants.FiftyPercent && PostTest >= DataConstants.FiftyPercent
                             ? 3
                             : 1;
                    }
                    else
                    {
                        criteriaMet = PreTest < DataConstants.FiftyPercent && PostTest < DataConstants.FiftyPercent
                             ? 0
                             : 1;
                    }
                }
                else if (!LVIReported)
                {
                    if (Observed > 0.2m)
                    {
                        criteriaMet = PreTest < DataConstants.FiftyPercent && PostTest < DataConstants.FiftyPercent
                             ? 3
                             : 1;
                    }
                    else
                    {
                        criteriaMet = PreTest >= DataConstants.FiftyPercent && PostTest >= DataConstants.FiftyPercent
                             ? 0
                             : 1;
                    }
                }

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
