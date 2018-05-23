using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Models
{
    public class Profile
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<CharacterDetails> AssociatedCharacters { get; set; }
    }
}
