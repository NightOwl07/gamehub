using Microsoft.Extensions.Logging;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Server.Contracts.Interfaces.Services;

namespace TTT.Server.Handler.Player
{
    public class CharacterHandler : ISingletonScript
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<CharacterHandler> _logger;

        public CharacterHandler(ILogger<CharacterHandler> logger, IInventoryService inventoryService)
        {
            this._logger = logger;
            this._inventoryService = inventoryService;
        }
    }
}