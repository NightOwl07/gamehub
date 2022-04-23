using System;
using System.Linq;
using AltV.Net;
using Newtonsoft.Json;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Contracts.Interfaces.Menu.Items;
using TTT.Core.Contracts.Menu.Items;
using TTT.Core.Entities;

namespace TTT.Server.Handler.Menu
{
    public class MenuHandler : ISingletonScript
    {
        public MenuHandler()
        {
            Alt.OnClient<TownPlayer, string, string>("TTT:MenuHandler:MenuInteraction", this.MenuInteraction);
            Alt.OnClient<TownPlayer>("TTT:MenuHandler:MenuClose", this.MenuClose);
        }

        private void MenuInteraction(TownPlayer player, string sItemData, string result)
        {
            IMenuItemData itemData = JsonConvert.DeserializeObject<MenuItemData>(sItemData);
            Console.WriteLine($"Interaction on ItemType {itemData?.ItemType} with id {itemData?.Id}");

            IUIMenuItem? item = player?.ActiveMenu?.Items?.FirstOrDefault(i => i.ItemData.Id == itemData?.Id);
            item?.ExecuteAction(result);
        }

        private void MenuClose(TownPlayer player)
        {
            player.ActiveMenu = null;
        }
    }
}