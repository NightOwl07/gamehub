using System;
using TTT.Core.Contracts.Enums;
using TTT.Core.Contracts.Interfaces.Menu.Items;

namespace TTT.Core.Contracts.Menu.Items
{
    public class MenuItemData : IMenuItemData
    {
        public MenuItemData()
        {
        }

        public MenuItemData(Guid id, MenuItemType itemType)
        {
            this.Id = id;
            this.ItemType = itemType;
        }

        public Guid Id { get; set; }

        public MenuItemType ItemType { get; set; }
    }
}