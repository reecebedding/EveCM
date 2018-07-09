using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Models.Bulletin
{
    public class Bulletin
    {
        [Key]
        public int Id { get; set; }
        public string AuthorId { get; set; }
        [NotMapped]
        public ApplicationUser Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
