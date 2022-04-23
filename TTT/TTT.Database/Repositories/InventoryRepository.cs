using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TTT.Database.Contracts.Interfaces.Repositories;
using TTT.Database.Contracts.Models;
using TTT.Database.Repositories.Base;

namespace TTT.Database.Repositories
{
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        private readonly ILogger<InventoryRepository> _logger;

        public InventoryRepository(ILogger<InventoryRepository> logger) : base()
        {
            this._logger = logger;
        }

        public async Task<Inventory> GetByCharacterId(int charId)
        {
            return new Inventory();
            //return await this._context.Inventories.FirstOrDefaultAsync(inv => inv.CharacterId == charId);
        }
    }
}