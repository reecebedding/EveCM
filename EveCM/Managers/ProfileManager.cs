using EveCM.Data.Repositories.Contracts;
using EveCM.Managers.Contracts;
using EveCM.Managers.Contracts.Profile;
using EveCM.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EveCM.Managers
{
    public class ProfileManager : IProfileManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICharacterRepository _characterRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProfileManager(UserManager<ApplicationUser> userManager, ICharacterRepository characterRepository, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _characterRepository = characterRepository;
            _signInManager = signInManager;
        }

        public void AssociateCharacter(ClaimsPrincipal principal, CharacterDetails character)
        {
            ApplicationUser user = _userManager.GetUserAsync(principal).Result;
            IEnumerable<CharacterDetails> existingCharacters = _characterRepository.GetCharactersForUser(user);

            try
            {
                _characterRepository.AddCharacterToAccount(user, character);
                if (existingCharacters.Count() == 0)
                {
                    AddClaim(user, "PrimaryCharacterId", character.CharacterID.ToString());
                }
            }
            catch (Data.Repositories.PSQL.Exceptions.CharacterAlreadyAssignedException ex)
            {
                throw new Exceptions.CharacterAlreadyAssignedException();
            }
        }

        public void AssociateCharacter(ClaimsPrincipal principal, int characterId)
        {
            CharacterDetails characterDetails = _characterRepository.GetCharacter(characterId);
            ApplicationUser user = _userManager.GetUserAsync(principal).Result;

            if (string.Equals(characterDetails.AccountID, user.Id.ToString()))
                AddClaim(user, "PrimaryCharacterId", characterId.ToString());
        }

        private void AddClaim(ApplicationUser user, string name, object value)
        {
            _userManager.AddClaimAsync(user, new Claim(name, value.ToString())).Wait();
            _userManager.UpdateAsync(user).Wait();
            _signInManager.RefreshSignInAsync(user).Wait();
        }
    }
}
