using System;
using System.ComponentModel.DataAnnotations;

namespace EveCM.Models.Bulletin.Dtos
{
    public class BulletinDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public AuthorCharacterDto AuthorCharacter { get; set; }
    }
}
