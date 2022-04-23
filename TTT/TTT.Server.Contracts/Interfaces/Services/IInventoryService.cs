using System.Threading.Tasks;
using TTT.Database.Contracts.Models;

namespace TTT.Server.Contracts.Interfaces.Services
{
    public interface IInventoryService
    {
        Task<Inventory> GetInventoryByCharacterId(int charId);

        Task SaveInventory(Inventory inventory);

        Task UpdateInventory(Inventory inventory);
    }
}