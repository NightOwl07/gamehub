using AltV.Net.Resources.Chat.Api;
using CustomCommandsSystem.Common.Attributes;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Entities;

namespace TTT.Server.Commands.Admin
{
    public class VanishCommand : ISingletonCommand
    {
        private readonly ILogger<VanishCommand> _logger;

        public VanishCommand(ILogger<VanishCommand> logger)
        {
            this._logger = logger;
        }

        [CustomCommand("vanish")]
        [CustomCommandAlias("invisible")]
        public static void VanishCmd(TownPlayer player)
        {
            if (Utils.Utils.CheckAdmin(player, TTT.Contracts.Base.Enums.PermissionLevel.Support))
            {
                player.Invincible = !player.Invincible;
                player.Visible = !player.Visible;
                player.SendChatMessage($"Godmode: {player.Invincible}" + $" Sichtbar: {player.Visible}");
            }
        }
    }
}