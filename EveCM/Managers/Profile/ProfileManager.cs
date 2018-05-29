using EveCM.Data.Repositories.Contracts;
using EveCM.Managers.Profile.Contracts;
using EveCM.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EveCM.Managers.Profile
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
            AssociateCharacter(user, character);
        }

        public void AssociateCharacter(ApplicationUser user, CharacterDetails character)
        {
            IEnumerable<CharacterDetails> existingCharacters = _characterRepository.GetCharactersForUser(user);

            try
            {
                _characterRepository.AddCharacterToAccount(user, character);
                if (existingCharacters.Count() == 0)
                    SetCharacterAsPrimary(user, character);

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
                SetCharacterAsPrimary(user, characterDetails);
        }

        public void SetCharacterAsPrimary(ApplicationUser user, CharacterDetails character)
        {
            user.PrimaryCharacterId = character.CharacterID.ToString();
            _userManager.UpdateAsync(user).Wait();
        }
    }
}
