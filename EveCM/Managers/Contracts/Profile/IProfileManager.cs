using EveCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EveCM.Managers.Contracts.Profile
{
    public interface IProfileManager
    {
        void AssociateCharacter(ClaimsPrincipal principal, CharacterDetails character);
        void AssociateCharacter(ClaimsPrincipal principal, int characterId);
    }
}
