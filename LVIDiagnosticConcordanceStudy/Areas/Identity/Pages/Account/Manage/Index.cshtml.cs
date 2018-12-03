﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<LVIStudyUser> _userManager;
        private readonly SignInManager<LVIStudyUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RequestLocalizationOptions _locOptions;

        public IndexModel(
            UserManager<LVIStudyUser> userManager,
            SignInManager<LVIStudyUser> signInManager,
            IEmailSender emailSender,
            IOptions<RequestLocalizationOptions> locOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _locOptions = locOptions.Value;
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
                    .Select(c => new SelectListItem { Value = c.Name, Text = c.DisplayName })
                    .ToList();
            }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "FirstNameRequired_Error")]
            [Display(Name = "First Name")]
            [DataType(DataType.Text, ErrorMessage = "Error")]
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
                            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Input.Culture)),
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
            StatusMessage = "Your profile has been updated";
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
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
