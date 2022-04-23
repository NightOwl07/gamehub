using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Base;
using TTT.Database.Contracts.Interfaces.Repositories;
using TTT.Database.Contracts.Models;
using TTT.Server.Contracts.Interfaces.Services;

namespace TTT.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository, ILogger<AccountService> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        public async Task<OperationResult<Account>> RegisterAccount(Account account)
        {
            OperationResult<Account> result = new();

            if (await this.DoesAccountExist(account))
            {
                Exception ex = new($"Can't register account {account.Username} because it already exists!");
                result.AddError(ex);
                return result;
            }

            await this._repository.InsertOneAsync(account);

            result.Result = account;

            this._logger.LogDebug($"Successfully created Account {account.Username} with ID #{account.Id}");

            return result;
        }

        public async Task<OperationResult<Account>> LogInAccount(string username, string passwordHash)
        {
            OperationResult<Account> result = new();

            Account account = await this._repository.FindOneAsync(acc => acc.Username == username || acc.Email == username);

            if (account == null)
            {
                Exception ex = new($"Account {username} doesn't exist!");
                result.AddError(ex);
                return result;
            }

            if (!BCrypt.Net.BCrypt.Verify(passwordHash, account.PasswordHash))
            {
                Exception ex = new("Password is wrong!");
                result.AddError(ex);
                return result;
            }

            this._logger.LogDebug($"Account {account.Username} with ID #{account.Id} successfully logged in!");

            account.LastLogin = DateTime.Now;
            await this._repository.ReplaceOneAsync(account);

            result.Result = account;

            return result;
        }

        #region Helper Methods

        private async Task<bool> DoesAccountExist(Account account)
        {
            return (await this._repository.FindOneAsync(acc => acc.Username == account.Username || acc.Email == account.Email)) != null;
        }

        #endregion
    }
}