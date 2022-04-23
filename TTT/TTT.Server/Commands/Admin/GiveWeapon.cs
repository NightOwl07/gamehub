using System;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using CustomCommandsSystem.Common.Attributes;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Entities;

namespace TTT.Server.Commands.Admin
{
    public class GiveWeapon : ISingletonCommand
    {
        private readonly ILogger<GiveWeapon> _logger;

        public GiveWeapon(ILogger<GiveWeapon> logger)
        {
            this._logger = logger;
        }

        [CustomCommand("giveweapon")]
        [CustomCommandAlias("weapon")]
        public void GiveWeaponCmd(TownPlayer player, string model)
        {
            player?.GiveWeapon(Alt.Hash(model), 999, false);
        }

        [CustomCommand("giveweapon")]
        [CustomCommandAlias("weapon")]
        public void GiveWeaponCmd(TownPlayer player)
        {
            foreach (WeaponModel weapon in (WeaponModel[])Enum.GetValues(typeof(WeaponModel)))
                player?.GiveWeapon(weapon, 999, false);
        }
    }
}