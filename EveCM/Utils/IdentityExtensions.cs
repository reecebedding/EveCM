using EveCM.Models;
using Microsoft.AspNetCore.Identity;
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
        public static string AvatarUrl(this ClaimsPrincipal identity, UserManager<ApplicationUser> userManager, EveImageHelper.CharacterAvatarSize imageSize = EveImageHelper.CharacterAvatarSize.Two_Fifty_Six)
        {
            string avatarUrl = string.Empty;
            var characterId = userManager.GetUserAsync(identity).Result.PrimaryCharacterId;
            if (string.IsNullOrEmpty(characterId))
                avatarUrl = "/images/guest.png";
            else
                avatarUrl = EveImageHelper.GetCharacterAvatar(characterId, imageSize).ToString();
            return avatarUrl;
        }

        public static string PrimaryCharacterId(this ClaimsPrincipal identity, UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = userManager.GetUserAsync(identity).Result;
            return user.PrimaryCharacterId;
        }
    }
}
