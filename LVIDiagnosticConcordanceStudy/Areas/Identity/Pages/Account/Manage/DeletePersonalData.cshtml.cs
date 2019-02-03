using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using LVIDiagnosticConcordanceStudy.Areas.Identity.Data;
using LVIDiagnosticConcordanceStudy.Data.Repository;
using LVIDiagnosticConcordanceStudy.Infrastructure.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace LVIDiagnosticConcordanceStudy.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<LVIStudyUser> _userManager;
        private readonly SignInManager<LVIStudyUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IRepository<ParticipantCode> _participantCodeRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public DeletePersonalDataModel(
            UserManager<LVIStudyUser> userManager,
            SignInManager<LVIStudyUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IRepository<ParticipantCode> participantCodeRepository,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _participantCodeRepository = participantCodeRepository;
            _sharedLocalizer = sharedLocalizer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer[$"Unable to load user with ID '{_userManager.GetUserId(User)}'."]);
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(_sharedLocalizer[$"Unable to load user with ID '{_userManager.GetUserId(User)}'."]);
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, _sharedLocalizer["Password not correct."]);
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
            FreeUpParticipantCode(user.Code);

            user.UserName = Guid.NewGuid().ToString();
            user.NormalizedUserName = "";
            user.Email = "withdrawn";
            user.NormalizedEmail = "WITHDRAWN";
            user.EmailConfirmed = false;
            user.Culture = null;
            user.FirstName = "Withdrawn";
            user.Gender = GenderEnum.NotSpecified;
            user.ParticipantCode = null;
            user.LastName = "Withdrawn";
            user.Nationality = "Withdrawn";
            user.PlaceOfWork = "Withdrawn";

            return await _userManager.UpdateAsync(user);
        }

        private void FreeUpParticipantCode(string code)
        {
            var participantCode = _participantCodeRepository.GetSingleBySpec(new ParticipantCodeSpecification(code, null));

            participantCode.IsUsed = false;

            _participantCodeRepository.Update(participantCode);

        }
    }
}