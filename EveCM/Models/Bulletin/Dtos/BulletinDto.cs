using System;

namespace EveCM.Models.Bulletin.Dtos
{
    public class BulletinDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public AuthorCharacterDto AuthorCharacter { get; set; }
    }
}
