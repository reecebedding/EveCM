using EveCM.Data.Repositories.Contracts;
using EveCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EveCM.Data.Repositories.PSQL.Exceptions;

namespace EveCM.Data.Repositories.PSQL
{
    public class CharacterRepository : ICharacterRepository
    {
        private EveCMContext db;
        public CharacterRepository(EveCMContext context)
        {
            db = context;
        }

        public void AddCharacterToAccount(ApplicationUser user, CharacterDetails character)
        {
            CharacterDetails exists = GetCharacter(character.CharacterID);

            if (exists == null)
            {
                character.AccountID = user.Id;
                db.CharacterDetails.Add(character);
                db.SaveChanges();
            }
            else
                throw new CharacterAlreadyAssignedException();
        }

        public CharacterDetails GetCharacter(int characterId)
        {
            return db.CharacterDetails.Where(x => x.CharacterID == characterId).FirstOrDefault();
        }

        public IEnumerable<CharacterDetails> GetCharactersForUser(ApplicationUser user)
        {
            return db.CharacterDetails.Where(x => x.AccountID == user.Id).ToList();
        }
    }
}
