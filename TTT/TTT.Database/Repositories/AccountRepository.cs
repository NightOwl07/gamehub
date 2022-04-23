using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Base;
using TTT.Database.Contracts.Interfaces.Repositories;
using TTT.Database.Contracts.Models;
using TTT.Database.Repositories.Base;

namespace TTT.Database.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(ILogger<AccountRepository> logger) : base()
        {
            this._logger = logger;
        }

        public OperationResult<IEnumerable<Character>> GetCharactersFromAccount(Account account)
        {
            OperationResult<IEnumerable<Character>> result = new();

            try
            {
               // result.Result = (from accounts in this._context.Accounts
               //     join characters in this._context.Characters
               //         on account.Id equals characters.AccountId
               //     select characters).ToList();
            }
            catch (Exception ex)
            {
                this._logger?.LogError($"Error fetching characters for account #{account?.Id}\n{ex}");
                result.AddError(ex);
            }

            return result;
        }
    }
}