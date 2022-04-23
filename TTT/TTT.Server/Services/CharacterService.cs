using Microsoft.Extensions.Logging;
using TTT.Database.Contracts.Interfaces.Repositories;
using TTT.Server.Contracts.Interfaces.Services;

namespace TTT.Server.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ILogger<CharacterService> _logger;
        private readonly ICharacterRepository _repository;

        public CharacterService(ICharacterRepository repository, ILogger<CharacterService> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
    }
}