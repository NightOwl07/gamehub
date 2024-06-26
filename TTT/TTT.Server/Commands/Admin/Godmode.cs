﻿using AltV.Net.Resources.Chat.Api;
using CustomCommandsSystem.Common.Attributes;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Entities;

namespace TTT.Server.Commands.Admin
{
    public class GodmodeCommand : ISingletonCommand
    {
        private readonly ILogger<GodmodeCommand> _logger;

        public GodmodeCommand(ILogger<GodmodeCommand> logger)
        {
            this._logger = logger;
        }

        [CustomCommand("godmode")]
        [CustomCommandAlias("god")]
        public void GiveWeaponCmd(TownPlayer player)
        {
            player.Invincible = !player.Invincible;
            player.SendChatMessage($"Godmode: {player.Invincible}");
        }
    }
}