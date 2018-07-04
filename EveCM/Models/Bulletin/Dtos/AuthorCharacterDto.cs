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
                return Utils.EveImageHelper.GetCharacterAvatar(CharacterId, Utils.EveImageHelper.CharacterAvatarSize.Thirty_Two);
            }
        }
    }
}
