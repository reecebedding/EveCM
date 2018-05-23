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

        public ProfileManager(UserManager<ApplicationUser> userManager, ICharacterRepository characterRepository)
        {
            _userManager = userManager;
            _characterRepository = characterRepository;
        }

        public void AssociateCharacter(ClaimsPrincipal principal, CharacterDetails character)
        {
            ApplicationUser user = _userManager.GetUserAsync(principal).Result;
            try
            {
                _characterRepository.AddCharacterToAccount(user, character);
            }
            catch (Data.Repositories.PSQL.Exceptions.CharacterAlreadyAssignedException ex)
            {
                throw new Exceptions.CharacterAlreadyAssignedException();
            }
        }
    }
}
