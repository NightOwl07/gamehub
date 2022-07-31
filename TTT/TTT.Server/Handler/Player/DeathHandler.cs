using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Entities;

namespace TTT.Server.Handler.Player
{
    public class DeathHandler : ISingletonScript
    {
        private readonly ILogger<DeathHandler> _logger;

        public DeathHandler(ILogger<DeathHandler> logger)
        {
            this._logger = logger;

            Alt.OnPlayerDead += (player, killer, u) =>
                Task.Run(() => this.OnPlayerDead(player as TownPlayer, killer, u));
        }

        private async Task OnPlayerDead(TownPlayer player, IEntity killer, uint weapon)
        {
            if (player == null)
                return;

            player.ToggleFadeScreen(2500, 2500);

            await Task.Delay(5000);

            player.Injuries?.Clear();
            player.RemoveAllWeapons();

            player.Spawn(new Position(0f, 0f, 75f));
        }
    }
}