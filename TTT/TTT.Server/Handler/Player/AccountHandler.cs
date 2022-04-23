using System;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Base;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Contracts.Interfaces.Entities;
using TTT.Core.Entities;
using TTT.Database.Contracts.Models;
using TTT.Server.Contracts.Interfaces.Services;

namespace TTT.Server.Handler.Player
{
    public class AccountHandler : ISingletonScript
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountHandler> _logger;
        private readonly INotificationService _notificationService;

        public AccountHandler(IAccountService accountService, INotificationService notificationService,
            ILogger<AccountHandler> logger)
        {
            this._accountService = accountService;
            this._notificationService = notificationService;
            this._logger = logger;

            Alt.OnPlayerConnect += (player, s) => Task.Run(() => this.OnPlayerConnect(player as TownPlayer, s));
            Alt.OnPlayerDisconnect += (player, s) => Task.Run(() => this.OnPlayerDisconnect(player as TownPlayer, s));
            AltAsync.OnClient<TownPlayer, string, string, string>("TTT:AccountHandler:Register",
                (player, email, username, password) =>
                    Task.Run(() => this.RegisterAccount(player, email, username, password)));
            AltAsync.OnClient<TownPlayer, string, string>("TTT:AccountHandler:Login", (player, username, password) =>
                Task.Run(() => this.LogInAccount(player, username, password)));
        }

        private async Task OnPlayerConnect(TownPlayer player, string reason)
        {
            await using (IAsyncContext asyncContext = AsyncContext.Create())
            {
                if (!player.TryToAsync(asyncContext, out IPlayer asyncPlayer)) return;

                this._logger.LogDebug($"Player \"{asyncPlayer.Name}\" connected");

                await Task.Delay(1000);

                asyncPlayer.Emit("TTT:AccountHandler:ShowWelcome");
                asyncPlayer.Visible = false;
            }
        }

        private async Task OnPlayerDisconnect(TownPlayer player, string reason)
        {
            if (player == null)
                return;

            this._logger.LogDebug($"Player \"{player.Account.Username}\" disconnected");
        }

        private async Task RegisterAccount(TownPlayer player, string email, string username, string password)
        {
            await using IAsyncContext asyncContext = AsyncContext.Create();

            if (!player.TryToAsync(asyncContext, out IPlayer asyncPlayer)) return;

            Account account = new()
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Email = email,
                CreatedOn = DateTime.Now,
                LastLogin = DateTime.Now,
                SocialClubId = asyncPlayer.SocialClubId,
                HardwareId = asyncPlayer.HardwareIdHash,
                HardwareIdEx = asyncPlayer.HardwareIdExHash
            };

            OperationResult<Account> result = await this._accountService.RegisterAccount(account);

            if (!(result?.Success ?? false))
            {
                this._notificationService.SendErrorNotification(asyncPlayer, "Fehler!", result?.FirstErrorMessage);
                asyncPlayer.Emit("TTT:AccountHandler:AuthenticationFailed");
                return;
            }

            player.Account = result.Result;

            this._notificationService.SendSuccessNotification(asyncPlayer, "Hurra!",
                "Du hast dich erfolgreich angemeldet!");

            asyncPlayer.Emit("TTT:AccountHandler:RegisterSuccess");
            await Task.Delay(200);
            asyncPlayer.Spawn(new Position(0, 0, 75));
        }

        private async Task LogInAccount(ITownPlayer player, string username, string password)
        {
            await using (IAsyncContext asyncContext = AsyncContext.Create())
            {
                if (!player.TryToAsync(asyncContext, out ITownPlayer asyncPlayer)) return;

                OperationResult<Account> result = await this._accountService.LogInAccount(username, password);

                if (!(result?.Success ?? false))
                {
                    this._notificationService.SendErrorNotification(asyncPlayer, "Fehler!", result?.FirstErrorMessage);
                    asyncPlayer.Emit("TTT:AccountHandler:AuthenticationFailed");
                    return;
                }

                asyncPlayer.Account = result.Result;
                asyncPlayer.SetStreamSyncedMetaData("Id", asyncPlayer.Account.Id.ToString());

                this._notificationService.SendSuccessNotification(asyncPlayer, "Hurra!",
                    "Du hast dich erfolgreich angemeldet!");

                Position spawn = new(0, 0, 75);

                asyncPlayer.Emit("TTT:PlayerHandler:SwitchInPlayer", false, spawn);
                asyncPlayer.Emit("TTT:AccountHandler:LoginSuccess");
                asyncPlayer.Visible = true;
                asyncPlayer.Spawn(new Position(0, 0, 75));
                await Task.Delay(2500);
                asyncPlayer.Emit("TTT:PlayerHandler:SwitchInPlayer", true, spawn);
            }
        }
    }
}