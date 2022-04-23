using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Entities;

namespace TTT.Server.Handler.Player
{
    public class DamageHandler : ISingletonScript
    {
        private readonly ILogger<DamageHandler> _logger;

        public DamageHandler(ILogger<DamageHandler> logger)
        {
            this._logger = logger;

            Alt.OnPlayerDamage += this.OnPlayerDamage;
            Alt.OnWeaponDamage += (player, target, u, damage, offset, part) =>
                this.OnWeaponDamage(player as TownPlayer, target, u, damage, offset, part);
            Alt.OnVehicleDamage += this.OnVehicleDamage;
        }

        private void OnVehicleDamage(IVehicle target, IEntity attacker, uint bodyhealthdamage,
            uint additionalbodyhealthdamage, uint enginehealthdamage, uint petroltankdamage, uint weaponhash)
        {
        }

        private bool OnWeaponDamage(TownPlayer player, IEntity target, uint weapon, ushort damage, Position shotoffset,
            BodyPart bodypart)
        {
            this._logger?.LogDebug($"player got weapon damaged on bodypart {nameof(bodypart)}");

            if (player?.Injuries?.ContainsKey(bodypart) ?? false)
                player.Injuries[bodypart] += damage;
            else
                player?.Injuries?.Add(bodypart, damage);

            return true;
        }

        private void OnPlayerDamage(IPlayer player, IEntity attacker, uint weapon, ushort healthdamage,
            ushort armourdamage)
        {
            this._logger?.LogDebug("player got damaged");
        }
    }
}