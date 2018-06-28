using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Models.Admin
{
    public class RoleInformationDto
    {
        public string Name { get; set; }
        public IEnumerable<UserInRoleDto> Users { get; set; }
    }
}
