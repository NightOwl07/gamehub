using CustomCommandsSystem.Common.Attributes;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Contracts.Interfaces.Menu;
using TTT.Core.Entities;

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
        }
    }
}