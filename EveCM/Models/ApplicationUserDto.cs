using System.Collections;
using System.Collections.Generic;

namespace EveCM.Models
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string PrimaryCharacterId { get; set; }
        public string AvatarUrl
        {
            get
            {
                return Utils.EveImageHelper.GetCharacterAvatar(PrimaryCharacterId).AbsoluteUri;
            }
        }
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
