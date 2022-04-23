using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TTT.Database.Contracts.Interfaces.Repositories;
using TTT.Database.Contracts.Models;
using TTT.Server.Contracts.Interfaces.Services;

namespace TTT.Server.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IInventoryItemRepository _itemRepository;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(ILogger<InventoryService> logger, IInventoryRepository inventoryRepository,
            IInventoryItemRepository itemRepository)
        {
            this._logger = logger;
            this._inventoryRepository = inventoryRepository;
            this._itemRepository = itemRepository;
        }

        public async Task<Inventory> GetInventoryByCharacterId(int charId)
        {
            return await this._inventoryRepository.GetByCharacterId(charId);
        }

        public async Task SaveInventory(Inventory inventory)
        {
            this._logger.LogDebug($"Saving inventory for character #{inventory.CharacterId}");
            await this._inventoryRepository.InsertOneAsync(inventory);
        }

        public async Task UpdateInventory(Inventory inventory)
        {
            this._logger.LogDebug($"Updating inventory for character #{inventory.CharacterId}");
            await this._inventoryRepository.ReplaceOneAsync(inventory);
        }
    }
}