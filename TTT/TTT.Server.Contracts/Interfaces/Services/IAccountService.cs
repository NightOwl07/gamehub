using System.Threading.Tasks;
using TTT.Contracts.Base;
using TTT.Database.Contracts.Models;

namespace TTT.Server.Contracts.Interfaces.Services
{
    public interface IAccountService
    {
        public Task<OperationResult<Account>> RegisterAccount(Account account);

        public Task<OperationResult<Account>> LogInAccount(string username, string passwordHash);
    }
}