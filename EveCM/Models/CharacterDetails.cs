using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Models
{
    public class CharacterDetails
    {
        [Key]
        public int CharacterID { get; set; }
        public string AccountID { get; set; }
        public string CharacterName { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string TokenType { get; set; }
        public string CharacterOwnerHash { get; set; }
    }
}
