using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EveCM.Data.Repositories.Contracts;
using EveCM.Managers.Contracts;
using EveCM.Managers.Contracts.Profile;
using EveCM.Models;
using EveCM.Models.AuthenticationViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EveCM.Controllers
{
    [Route("auth")]
    [Authorize]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOAuthManager _oAuthManager;
        private readonly ICharacterRepository _characterRepository;
        private readonly IProfileManager _profileManager;

        public AuthenticationController(
            UserManager<ApplicationUser> userManager,
            ILogger<AuthenticationController> logger,
            SignInManager<ApplicationUser> signInManager,
            IOAuthManager oAuthManager,
            ICharacterRepository characterRepository,
            IProfileManager profileManager)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _oAuthManager = oAuthManager;
            _characterRepository = characterRepository;
            _profileManager = profileManager;
        }

        [HttpGet("register")]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (TempData.Peek("AssociateCharacterWithRegistration") != null)
                    {
                        try
                        {
                            CharacterDetails character = JsonConvert.DeserializeObject<CharacterDetails>(TempData["AssociateCharacterWithRegistration"].ToString());
                            _profileManager.AssociateCharacter(user, character);
                        }
                        catch (Exception)
                        {
                            _logger.LogError("Unable to associate character when logging in with existing account");
                        }
                    }

                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    if (TempData.Peek("AssociateCharacterWithRegistration") != null)
                    {
                        try
                        {
                            CharacterDetails character = JsonConvert.DeserializeObject<CharacterDetails>(TempData["AssociateCharacterWithRegistration"].ToString());
                            ApplicationUser user = _userManager.FindByNameAsync(model.UserName).Result;
                            _profileManager.AssociateCharacter(user, character);
                        }
                        catch (Exception)
                        {
                            _logger.LogError("Unable to associate character when logging in with existing account");
                        }
                    }
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet("callback")]
        [AllowAnonymous]
        public IActionResult OAuthCallback(string code)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("AssociateCode", "Profile", new { code });
            else
            {
                CharacterDetails authenticatedCharacter = _oAuthManager.GetCharacterDetailsFromCode(code);
                CharacterDetails associatedCharacter = _characterRepository.GetCharacter(authenticatedCharacter.CharacterID);
                if (associatedCharacter != null)
                {
                    ApplicationUser user = _userManager.FindByIdAsync(associatedCharacter.AccountID).Result;
                    _signInManager.SignInAsync(user, true).Wait();
                }
                else
                {
                    return ExternalLoginRegistration(authenticatedCharacter);
                }

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet("external-login-register")]
        [AllowAnonymous]
        public IActionResult ExternalLoginRegistration(CharacterDetails characterDetails)
        {
            TempData["AssociateCharacterWithRegistration"] = JsonConvert.SerializeObject(characterDetails);

            return View("ExternalLoginRegistration", characterDetails);
        }

        [HttpGet("external-login")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin()
        {
            return Redirect(_oAuthManager.EVERedirectUrl());
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}