using CustomCommandsSystem.Common.Attributes;
using Microsoft.Extensions.Logging;
using System.Drawing;
using AltV.Net.Resources.Chat.Api;
using System.Threading.Tasks;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Contracts.Interfaces.Menu;
using TTT.Core.Entities;
using TTT.Server.Commands;
using TTT.Contracts.Base.Enums;


namespace TTT.Server.Commands.Admin
{
    public class AdminMenu : ISingletonCommand
    {
        private readonly ILogger<AdminMenu> _logger;
        private readonly IMenuBuilder _menuBuilder;

        public AdminMenu(ILogger<AdminMenu> logger, IMenuBuilder menuBuilder)
        {
            this._logger = logger;
            this._menuBuilder = menuBuilder;
        }

        [CustomCommand("amenu")]
        [CustomCommandAlias("adminmenu")]
        public void ShowAdminMenu(TownPlayer player)
        {
            // TODO: add check for admin level
            if (Utils.Utils.CheckAdmin(player, PermissionLevel.Support))
            {
                this._menuBuilder.Create("Admin Menu", "Admin stuff", new Point(150, 150))
                    .AddItem("Godmode", "Macht dich Unsterlich!", "",
                    (item, s) => { GodmodeCommand.GodmodeCmd(player); })
                    .Show(player);
            }
        }
    }
}