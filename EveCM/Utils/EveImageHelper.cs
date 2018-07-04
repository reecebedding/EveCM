using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Utils
{
    public static class EveImageHelper
    {
        public enum CharacterAvatarSize { Ten_Twenty_Four = 1024, Five_Hundred_Twelve = 512, Two_Fifty_Six = 256, One_Twenty_Eight = 128, Sixty_Four = 64, Thirty_Two = 32 }
        public static string GetCharacterAvatar(string characterId, CharacterAvatarSize size = CharacterAvatarSize.Two_Fifty_Six)
        {
            string avatarUri = "images/guest.png";

            if (!string.IsNullOrEmpty(characterId))
                avatarUri = $"https://image.eveonline.com/Character/{characterId}_{(int)size}.jpg";
            
            return avatarUri;
        }
    }
}
