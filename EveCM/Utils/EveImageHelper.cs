using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evecm.Utils
{
    public static class EveImageHelper
    {
        public enum CharacterAvatarSize { Five_Hundred_Twelve = 512, Two_Fifty_Six = 256, One_Twenty_Eight = 128, Sixty_Four = 64, Thirty_Two = 32 }
        public static Uri GetCharacterAvatar(string characterId, CharacterAvatarSize size = CharacterAvatarSize.Two_Fifty_Six)
        {
            Uri avatarUri = new Uri($"https://image.eveonline.com/Character/{characterId}_{(int)size}.jpg");
            return avatarUri;
        }
    }
}
