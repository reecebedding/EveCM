using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EveCM.Data.Repositories.Contracts;
using EveCM.Managers.Contracts;
using EveCM.Managers.Contracts.Profile;
using EveCM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static EveCM.Managers.Contracts.Profile.Exceptions;

namespace EveCM.Controllers
{
    [Authorize]
    [Route("profile")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOAuthManager _oauthManager;
        private readonly IProfileManager _profileManager;
        private readonly ICharacterRepository _characterRepository;
        private readonly EveSettings _eveSettings;

        public ProfileController(
            UserManager<ApplicationUser> userManager, 
            IOptions<EveSettings> eveSettings, 
            IOAuthManager oauthManager, 
            IProfileManager profileManager,
            ICharacterRepository characterRepository)
        {
            _userManager = userManager;
            _oauthManager = oauthManager;
            _profileManager = profileManager;
            _characterRepository = characterRepository;
            _eveSettings = eveSettings.Value;
        }
        public IActionResult Index(string errorMessage = null)
        {
            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            Profile profile = new Profile()
            {
                UserName = User.Identity.Name,
                Email = user.Email,
                AssociatedCharacters = _characterRepository.GetCharactersForUser(user)
            };

            if (!string.IsNullOrEmpty(errorMessage))
                ModelState.AddModelError("", errorMessage);
            
            return View(profile);
        }

        [HttpGet("associate")]
        public IActionResult Associate()
        {
            return Redirect(_oauthManager.EVERedirectUrl());
        }

        [HttpGet("associate-code")]
        public IActionResult AssociateCode(string code)
        {
            CharacterDetails character =  _oauthManager.GetCharacterDetailsFromCode(code);

            string errorMessage = string.Empty;
            try
            {
                _profileManager.AssociateCharacter(User, character);
            }
            catch (CharacterAlreadyAssignedException ex)
            {
                errorMessage = "Character already assigned to an account";
            }
            catch (Exception ex)
            {
                errorMessage = "An error occured. Please try again.";
            }

            return RedirectToAction("Index", new { errorMessage });
        }
    }
}