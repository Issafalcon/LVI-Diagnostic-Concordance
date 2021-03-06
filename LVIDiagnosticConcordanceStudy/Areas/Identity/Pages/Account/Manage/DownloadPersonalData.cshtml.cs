﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<LVIStudyUser> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public DownloadPersonalDataModel(
            UserManager<LVIStudyUser> userManager,
            ILogger<DownloadPersonalDataModel> logger,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _userManager = userManager;
            _logger = logger;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer[$"Unable to load user with ID '{_userManager.GetUserId(User)}'"]);
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(LVIStudyUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(personalData)), "text/json");
        }
    }
}
