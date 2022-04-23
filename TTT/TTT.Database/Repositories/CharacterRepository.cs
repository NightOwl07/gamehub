using TTT.Database.Contracts.Interfaces.Repositories;
using TTT.Database.Contracts.Models;
using TTT.Database.Repositories.Base;

namespace TTT.Database.Repositories
{
    public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
    {
        public CharacterRepository() : base()
        {
        }
    }
}