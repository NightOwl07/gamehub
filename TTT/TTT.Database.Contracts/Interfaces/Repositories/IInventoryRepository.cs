using System.Threading.Tasks;
using TTT.Database.Contracts.Interfaces.Repositories.Base;
using TTT.Database.Contracts.Models;

namespace TTT.Database.Contracts.Interfaces.Repositories
{
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        Task<Inventory> GetByCharacterId(int charId);
    }
}