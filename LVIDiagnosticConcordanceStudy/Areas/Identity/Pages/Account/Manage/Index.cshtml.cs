using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Pages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<LVIStudyUser> _userManager;
        private readonly SignInManager<LVIStudyUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RequestLocalizationOptions _locOptions;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public IndexModel(
            UserManager<LVIStudyUser> userManager,
            SignInManager<LVIStudyUser> signInManager,
            IEmailSender emailSender,
            IOptions<RequestLocalizationOptions> locOptions,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _locOptions = locOptions.Value;
            _sharedLocalizer = sharedLocalizer;
        }

        [TempData]
        public string Username { get; set; }

        [TempData]
        public bool IsEmailConfirmed { get; set; }

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

        public List<SelectListItem> GenderSelect
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "Female", Text = _sharedLocalizer["Female"].Value },
                    new SelectListItem { Value = "Male", Text = _sharedLocalizer["Male"].Value },
                    new SelectListItem { Value = "Other", Text = _sharedLocalizer["Other"].Value }
                };
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

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Required_Field_Error")]
            [EmailAddress(ErrorMessage = "Email_Validation_Error")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "First Name")]
            [DataType(DataType.Text, ErrorMessage = "Error")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Gender")]
            public string Gender { get; set; }

            [PersonalData]
            [Display(Name = "Nationality")]
            [Required(ErrorMessage = "Required_Field_Error")]
            public string Nationality { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Preferred Language")]
            public string Culture { get; set; }

            [PersonalData]
            [Display(Name = "Place of Work (Hospital)")]
            public string PlaceOfWork { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "YearsQualified_Field_Error")]
            [Display(Name = "Years Qualified", Description = "The number of years you have been qualified as a doctor")]
            public int YearsQualified { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "YearsInPath_Field_Error")]
            [Display(Name = "Years in Histopathology", Description = "The number of years you have been working in the specialty of histopathology")]
            public int YearsInPath { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "Required_Field_Error")]
            [Display(Name = "Breast Subspecialty ?")]
            public bool IsBreastSpecialist { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);

            var userName = await _userManager.GetUserNameAsync(user);
            Username = userName;

            Input = new InputModel
            {
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Nationality = user.Nationality,
                Culture = user.Culture,
                PlaceOfWork = user.PlaceOfWork,
                YearsQualified = user.YearsQualified,
                YearsInPath = user.YearsInPath,
                IsBreastSpecialist = user.IsBreastSpecialist
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            if (Input.Gender != user.Gender)
            {
                user.Gender = Input.Gender;
            }

            if (Input.Nationality != user.Nationality)
            {
                user.Nationality = Input.Nationality;
            }

            if (Input.Gender != user.Gender)
            {
                user.Gender = Input.Gender;
            }

            if (Input.Culture != user.Culture)
            {
                user.Culture = Input.Culture ?? "en-GB";

                Response.Cookies.Append(
                            CookieRequestCultureProvider.DefaultCookieName,
                            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(user.Culture)),
                            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                        );
            }

            if (Input.PlaceOfWork != user.PlaceOfWork)
            {
                user.PlaceOfWork = Input.PlaceOfWork;
            }

            if (Input.YearsQualified != user.YearsQualified)
            {
                user.YearsQualified = Input.YearsQualified;
            }

            if (Input.YearsInPath != user.YearsInPath)
            {
                user.YearsInPath = Input.YearsInPath;
            }

            if (Input.IsBreastSpecialist != user.IsBreastSpecialist)
            {
                user.IsBreastSpecialist = Input.IsBreastSpecialist;
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = _sharedLocalizer["Your profile has been updated"];
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                _sharedLocalizer["Confirm your email"],
                string.Format(_sharedLocalizer["Email_Confirmation_Message_Text"], HtmlEncoder.Default.Encode(callbackUrl)));
                //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = _sharedLocalizer["Verification email sent. Please check your email."];
            return RedirectToPage();
        }
    }
}
