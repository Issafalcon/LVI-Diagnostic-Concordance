using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LVIDiagnosticConcordanceStudy.Models.ViewComponents
{
    public class InterventionViewComponent : ViewComponent
    {
        public class Probabilities
        {
            public decimal PreTest { get; private set; }
            public decimal PostTest { get; private set; }
            public decimal Observed { get; private set; }
            public int ConcordanceCriteriaMet { get; private set; }

            public Probabilities(decimal preTestProbability, decimal postTestProbability, decimal observed)
            {
                PreTest = preTestProbability;
                PostTest = postTestProbability;
                Observed = observed;
            }

            public void CalculateConcordance()
            {
                int criteriaMet = 0;
                criteriaMet += this.PreTest < DataConstants.SecondInterval ? 0 : 1;
                criteriaMet += this.PostTest < DataConstants.SecondInterval ? 0 : 1;
                criteriaMet += this.Observed < 0.2m ? 0 : 1;

                //ConcordanceCriteriaMet = criteriaMet;
            }
        }

        public IViewComponentResult Invoke(decimal preTestProbability, decimal postTestProbability, decimal observed)
        {
            Probabilities probabilityData = new Probabilities(preTestProbability, postTestProbability, observed);
            probabilityData.CalculateConcordance();

            return View(probabilityData);
        }
    }

}
