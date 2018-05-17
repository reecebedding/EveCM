using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace evecm.Utils
{
    public static class IdentityExtensions
    {
        public static string PortraitUrl(this IIdentity identity, EveImageHelper.CharacterAvatarSize imageSize = EveImageHelper.CharacterAvatarSize.Two_Fifty_Six)
        {
            var characterId = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var avatarUri = EveImageHelper.GetCharacterAvatar(characterId, imageSize);
            return avatarUri.ToString();
        }
    }
}
