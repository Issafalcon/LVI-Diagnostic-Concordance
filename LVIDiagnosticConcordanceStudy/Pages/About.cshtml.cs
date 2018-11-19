using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace LVIDiagnosticConcordanceStudy.Pages
{
    [AllowAnonymous]
    public class AboutModel : PageModel
    {
        private readonly IStringLocalizer<AboutModel> _localizer;

        public string Message { get; set; }

        public AboutModel(IStringLocalizer<AboutModel> localizer)
        {
            _localizer = localizer;
        }
        public void OnGet()
        {
            Message = _localizer["Your application description page."];
        }
    }
}
