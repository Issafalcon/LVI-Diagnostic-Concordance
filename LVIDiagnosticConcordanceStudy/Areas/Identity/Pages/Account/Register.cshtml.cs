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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Services;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using LVIDiagnosticConcordanceStudy.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using LVIDiagnosticConcordanceStudy.Pages;

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
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        //CODE_FEATURE
        //private readonly IAsyncRepository<ParticipantCode> _codeRepository;
        private readonly RequestLocalizationOptions _locOptions;
        private readonly StudyOptions _studyOptions;

        public RegisterModel(
            UserManager<LVIStudyUser> userManager,
            SignInManager<LVIStudyUser> signInManager,
            IUserService userService,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IStringLocalizer<SharedResource> sharedLocalizer,
            //IAsyncRepository<ParticipantCode> codeRepository,
            IOptions<RequestLocalizationOptions> locOptions,
            IOptions<StudyOptions> studyOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _logger = logger;
            _emailSender = emailSender;
            _sharedLocalizer = sharedLocalizer;
            //_codeRepository = codeRepository;
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
                    .Select(c => new SelectListItem { Value = c.Name, Text = _sharedLocalizer[c.DisplayName] })
                    .ToList();
            }
        }
        
        public List<SelectListItem> Nationalities
        {
            get
            {
                return NationalitySelectList.Nationalities
                    .Select(n => new SelectListItem { Value = n, Text = _sharedLocalizer[n] })
                    .ToList();
            }
        }

        public List<SelectListItem> YesNoDropDown
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "true", Text = _sharedLocalizer["Yes"].Value },
                    new SelectListItem { Value = "false", Text = _sharedLocalizer["No"].Value }
                };
            }
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Required_Field_Error")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            //CODE_FEATURE
            //[Required(ErrorMessage = "Required_Field_Error")]
            //[Display(Name = "Participant Code", Description = "The 10 character code provided to you by the study administrator.")]
            //public string Code { get; set; }

            [Required(ErrorMessage = "Password_Required_Field_Error")]
            [StringLength(100, ErrorMessage = "New_Password_Error", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "New_Password_Match_Error")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [PersonalData]
            [Display(Name = "Gender")]
            [Required(ErrorMessage = "Required_Field_Error")]
            public GenderEnum Gender { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Nationality")]
            public string Nationality { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Preferred Language")]
            public string Culture { get; set; }

            [PersonalData]
            [Display(Name = "Place of Work (Hospital)")]
            public string PlaceOfWork { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Years Qualified", Description = "The number of years you have been qualified as a doctor")]
            public int YearsQualified { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Years in Histopathology", Description = "The number of years you have been working in the specialty of histopathology")]
            public int YearsInPath { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Breast Subspecialty ?")]
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

        //CODE_FEATURE
        //private async Task<ParticipantCode> GetMatchingCode(string code)
        //{
        //    var validCodes = await _codeRepository.ListAsync(new ParticipantCodeSpecification(null, false));

        //    return validCodes.FirstOrDefault(pc => pc.Code == code);
        //}

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

                //CODE_FEATURE
                //var matchingCode = await GetMatchingCode(user.Code);

                //if (matchingCode == null)
                //{
                //    ModelState.AddModelError("ParticipantCode", "The Participant Code you have provided is not valid");
                //    return Page();
                //}

                await RandomizeIntoGroup(user);

                user.UserName = user.Email;
                user.IsAdmin = false;

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Set the culture in the cookie here, so when the user is redirected, the preferred language is stored
                    string selectedCulture = user.Culture ?? "en-GB";

                    Response.Cookies.Append(
                                CookieRequestCultureProvider.DefaultCookieName,
                                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(selectedCulture)),
                                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                            );

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    var emailBody = _sharedLocalizer
                        .WithCulture(new CultureInfo(selectedCulture))["Email_Confirmation_Message_Text", HtmlEncoder.Default.Encode(callbackUrl)];

                    await _emailSender.SendEmailAsync(Input.Email, _sharedLocalizer
                        .WithCulture(new CultureInfo(selectedCulture))["Confirm your email"], emailBody);

                    //CODE_FEATURE
                    // Set the existing code status to 'Used' and update the DB
                    //matchingCode.IsUsed = true;
                    //await _codeRepository.UpdateAsync(matchingCode);

                    return RedirectToPage("./RegisterConfirmation");
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
