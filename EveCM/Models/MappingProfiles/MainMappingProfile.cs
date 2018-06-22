using EveCM.Models.Bulletin;
using EveCM.Models.Bulletin.Dtos;

namespace EveCM.Models.MappingProfiles
{
    public class MainMappingProfile : AutoMapper.Profile
    {
        public MainMappingProfile()
        {
            CreateMap<Bulletin.Bulletin, BulletinDto>()
                .ForPath(x => x.AuthorCharacter.CharacterId, opt => opt.MapFrom(y => y.Author.PrimaryCharacterId))
                .ForPath(x => x.AuthorCharacter.UserName, opt => opt.MapFrom(y => y.Author.UserName))
                .ReverseMap();
        }
    }
}
