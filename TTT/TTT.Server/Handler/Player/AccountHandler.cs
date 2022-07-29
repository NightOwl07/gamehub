using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
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
            this._logger.LogDebug($"Player \"{player.Name}\" connected");

            await Task.Delay(1000);

            player.Emit("TTT:AccountHandler:ShowWelcome");
            player.Visible = false;
        }

        private async Task OnPlayerDisconnect(TownPlayer player, string reason)
        {
            if (player?.Account == null)
            {
                this._logger.LogDebug($"Player \"{player?.Name}\" disconnected");
                return;
            }

            this._logger.LogDebug($"Player \"{player.Account.Username}\" disconnected");
        }

        private async Task RegisterAccount(TownPlayer player, string email, string username, string password)
        {
            Account account = new()
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Email = email,
                CreatedOn = DateTime.Now,
                LastLogin = DateTime.Now,
                SocialClubId = player.SocialClubId,
                HardwareId = player.HardwareIdHash,
                HardwareIdEx = player.HardwareIdExHash
            };

            OperationResult<Account> result = await this._accountService.RegisterAccount(account);

            if (!(result?.Success ?? false))
            {
                this._notificationService.SendErrorNotification(player, "Fehler!", result?.FirstErrorMessage);
                player.Emit("TTT:AccountHandler:AuthenticationFailed", result?.FirstErrorMessage);
                return;
            }

            player.Account = result.Result;

            this._notificationService.SendSuccessNotification(player, "Hurra!",
                "Du hast dich erfolgreich angemeldet!");

            player.Emit("TTT:AccountHandler:RegisterSuccess");
            await Task.Delay(200);
            player.Spawn(new Position(0, 0, 75));
        }

        private async Task LogInAccount(ITownPlayer player, string username, string password)
        {
            OperationResult<Account> result = await this._accountService.LogInAccount(username, password);

            if (!(result?.Success ?? false))
            {
                this._notificationService.SendErrorNotification(player, "Fehler!", result?.FirstErrorMessage);
                player.Emit("TTT:AccountHandler:AuthenticationFailed");
                return;
            }

            player.Account = result.Result;
            player.SetStreamSyncedMetaData("Id", player.Account.Id.ToString());

            this._notificationService.SendSuccessNotification(player, "Hurra!",
                "Du hast dich erfolgreich angemeldet!");

            Position spawn = new(0, 0, 75);

            player.Emit("TTT:PlayerHandler:SwitchInPlayer", false, spawn);
            player.Emit("TTT:AccountHandler:LoginSuccess");
            player.Visible = true;
            player.Spawn(new Position(0, 0, 75));
            await Task.Delay(2500);
            player.Emit("TTT:PlayerHandler:SwitchInPlayer", true, spawn);
        }
    }
}