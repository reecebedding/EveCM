using EveCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EveCM.Managers.Profile.Contracts
{
    public interface IProfileManager
    {
        void AssociateCharacter(ClaimsPrincipal principal, CharacterDetails character);
        void AssociateCharacter(ApplicationUser user, CharacterDetails character);
        void AssociateCharacter(ClaimsPrincipal principal, int characterId);
        void SetCharacterAsPrimary(ApplicationUser user, CharacterDetails character);
    }
}
