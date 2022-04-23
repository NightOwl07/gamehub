using System.Collections.Generic;
using TTT.Contracts.Base;
using TTT.Database.Contracts.Interfaces.Repositories.Base;
using TTT.Database.Contracts.Models;

namespace TTT.Database.Contracts.Interfaces.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
    }
}