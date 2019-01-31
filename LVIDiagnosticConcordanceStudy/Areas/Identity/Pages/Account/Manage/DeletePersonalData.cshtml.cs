using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<LVIStudyUser> _userManager;
        private readonly SignInManager<LVIStudyUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;

        public DeletePersonalDataModel(
            UserManager<LVIStudyUser> userManager,
            SignInManager<LVIStudyUser> signInManager,
            ILogger<DeletePersonalDataModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Password not correct.");
                    return Page();
                }
            }
            var result = await AnonymiseUserAsync(user);

            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred anonymising user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }

        private async Task<IdentityResult> AnonymiseUserAsync(LVIStudyUser user)
        {
            user.UserName = Guid.NewGuid().ToString();
            user.NormalizedUserName = "";
            user.Email = "withdrawn";
            user.NormalizedEmail = "WITHDRAWN";
            user.EmailConfirmed = false;
            user.Culture = null;
            user.FirstName = "Withdrawn";
            user.Gender = GenderEnum.NotSpecified;
            user.LastName = "Withdrawn";
            user.Nationality = "Withdrawn";
            user.PlaceOfWork = "Withdrawn";

            return await _userManager.UpdateAsync(user);
        }
    }
}