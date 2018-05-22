using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace EveCM.Utils
{
    public static class IdentityExtensions
    {
        public static string PortraitUrl(this IIdentity identity, EveImageHelper.CharacterAvatarSize imageSize = EveImageHelper.CharacterAvatarSize.Two_Fifty_Six)
        {
            string avatarUrl = string.Empty;
            var characterId = identity.PrimaryCharacterId();
            if (string.IsNullOrEmpty(characterId))
                avatarUrl = "/images/guest.png";
            else
                avatarUrl = EveImageHelper.GetCharacterAvatar(characterId, imageSize).ToString();
            return avatarUrl;
        }

        public static string PrimaryCharacterId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("PrimaryCharacterId");
            return claim?.Value ?? string.Empty;
        }
    }
}
