using Newtonsoft.Json;

namespace EveCM.Models.Bulletin.Dtos
{
    public class AuthorCharacterDto
    {
        [JsonIgnore]
        public string CharacterId { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl
        {
            get
            {
                string avatarUrl = "images/guest.png";
                if (!string.IsNullOrEmpty(CharacterId))
                    avatarUrl = Utils.EveImageHelper.GetCharacterAvatar(CharacterId, Utils.EveImageHelper.CharacterAvatarSize.Thirty_Two).AbsoluteUri;
                return avatarUrl;
            }
        }
    }
}
