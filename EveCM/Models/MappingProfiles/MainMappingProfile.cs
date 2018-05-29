using EveCM.Models.Bulletin;
using EveCM.Models.Bulletin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Models.MappingProfiles
{
    public class MainMappingProfile : AutoMapper.Profile
    {
        public MainMappingProfile()
        {
            CreateMap<Notification, NotificationViewModel>().ReverseMap();
        }
    }
}
