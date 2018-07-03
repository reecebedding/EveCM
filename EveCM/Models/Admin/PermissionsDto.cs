using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Models.Admin
{
    public class PermissionsDto
    {
        public IEnumerable<string> Roles { get; set; }
    }
}
