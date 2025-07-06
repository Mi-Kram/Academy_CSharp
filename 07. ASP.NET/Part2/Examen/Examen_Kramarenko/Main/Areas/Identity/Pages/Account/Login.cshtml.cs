using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Main.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Main.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public class InputModel
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
			if (User.Identity.IsAuthenticated)
			{
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (await _userManager.IsInRoleAsync(user, "Admin")) return LocalRedirect(Url.Action("Index", "Admin"));
                if (await _userManager.IsInRoleAsync(user, "Master")) return LocalRedirect(Url.Action("Index", "Home"));
			}

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            return Page();
        }

        public async Task<JsonResult> OnPostTryLoginAsync(InputModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
                    if (await _userManager.IsInRoleAsync(user, "Admin")) return new JsonResult(new { url = Url.Action("Index", "Admin") });
                    return new JsonResult(new { url = Url.Action("Index", "Home") });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверные данные!");
                }
            }

            return new JsonResult(new
            {
                errors = ModelState
                .Where(x => x.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                .Select(x => new
                {
                    key = x.Key,
                    value = x.Value.Errors?.FirstOrDefault()?.ErrorMessage ?? ""
                })
            });
        }
    }
}
