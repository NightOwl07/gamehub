using TTT.Database.Contracts.Interfaces.Repositories;
using TTT.Database.Contracts.Models;
using TTT.Database.Repositories.Base;

namespace TTT.Database.Repositories
{
    public class InventoryItemRepository : BaseRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository() : base()
        {
        }
    }
}