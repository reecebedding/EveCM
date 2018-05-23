using EveCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Data.Repositories.Contracts
{
    public interface ICharacterRepository
    {
        void AddCharacterToAccount(ApplicationUser user, CharacterDetails character);
        CharacterDetails GetCharacter(int characterId);
        IEnumerable<CharacterDetails> GetCharactersForUser(ApplicationUser user);
    }
}
