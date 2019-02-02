using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using LVIDiagnosticConcordanceStudy.Infrastructure.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using System.Security.Claims;
using LVIDiagnosticConcordanceStudy.Infrastructure.Security;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Services;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Infrastructure;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<LVIStudyUser> _signInManager;
        private readonly UserManager<LVIStudyUser> _userManager;
        private readonly IUserService _userService;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<RegisterModel> _localizer;
        private readonly IAsyncRepository<ParticipantCode> _codeRepository;
        private readonly RequestLocalizationOptions _locOptions;
        private readonly StudyOptions _studyOptions;

        public RegisterModel(
            UserManager<LVIStudyUser> userManager,
            SignInManager<LVIStudyUser> signInManager,
            IUserService userService,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IStringLocalizer<RegisterModel> localizer,
            IOptions<RequestLocalizationOptions> locOptions,
            IOptions<StudyOptions> studyOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _logger = logger;
            _emailSender = emailSender;
            _localizer = localizer;
            _locOptions = locOptions.Value;
            _studyOptions = studyOptions.Value;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        
        public List<SelectListItem> Cultures
        {
            get
            {
                return _locOptions.SupportedUICultures
                    .Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName })
                    .ToList();
            }
        }
            
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Participant Code", Description = "The 10 character code provided to you by the study administrator.")]
            public string Code { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "FirstNameRequired_Error")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "LastNameRequired_Error")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "GenderRequired_Error")]
            public GenderEnum Gender { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "NationalityRequired_Error")]
            public string Nationality { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Please select your preferred language")]
            [Display(Name = "Preferred Language")]
            public string Culture { get; set; }

            [PersonalData]
            [Display(Name = "Work Place (Hospital)")]
            public string PlaceOfWork { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Please enter the number of years you have been qualified")]
            [Display(Name = "Years Qualified", Description = "The number of years you have been qualified as a doctor")]
            public int YearsQualified { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Please enter the number of years you have worked in histopathology")]
            [Display(Name = "Years In Histopathology", Description = "The number of years you have been working in the specialty of histopathology")]
            public int YearsInPath { get; set; }

            [PersonalData]
            [Display(Name = "Subspecialty In Breast Pathology", Description = "Check the box if you subspecialise in breast pathology")]
            public bool IsBreastSpecialist { get; set; }
        }

        private async Task RandomizeIntoGroup(LVIStudyUser newParticipant)
        {
            Random random = new Random();

            int controlFlag = random.Next(0, 2);
            var participants = await _userService.GetUserListAsync();

            int participantGroupMax = _studyOptions.MaximumParticipants / 2;
            switch (controlFlag)
            {
                case 0:
                    int controlGroupCount = (from participant in participants
                                             where participant.InControlGroup == true
                                                && participant.IsAdmin == false
                                             select participant).Count();
                    if (controlGroupCount >= participantGroupMax)
                    {
                        newParticipant.InControlGroup = false;
                    }
                    else
                    {
                        newParticipant.InControlGroup = true;
                    }

                    break;
                case 1:
                    int interventionGroupCount = (from participant in participants
                                                 where participant.InControlGroup == false
                                                    && participant.IsAdmin == false
                                                 select participant).Count();
                    if (interventionGroupCount >= participantGroupMax)
                    {
                        newParticipant.InControlGroup = true;
                    }
                    else
                    {
                        newParticipant.InControlGroup = false;
                    }

                    break;
                default:
                    break;
            }
        }

        private async Task<bool> CheckValidParticipantCode(string code)
        {
            var validCodes = await _codeRepository.ListAsync(new ParticipantCodeSpecification(isUsed: false));

            return validCodes.Any(pc => pc.Code == code);
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new LVIStudyUser();

                await TryUpdateModelAsync<LVIStudyUser>(user, "input");

                if (!await CheckValidParticipantCode(user.ParticipantCode.Code))
                {
                    ModelState.AddModelError("ParticipantCode", "The Participant Code you have provided is not valid");
                    return Page();
                }

                await RandomizeIntoGroup(user);

                user.UserName = user.Email;
                user.IsAdmin = false;

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //TODO: Redirect to the Registration Confirmation Page - Asking user to check email and confirm address before logging in
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
