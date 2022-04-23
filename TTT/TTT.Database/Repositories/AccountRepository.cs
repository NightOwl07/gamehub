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
    }
}