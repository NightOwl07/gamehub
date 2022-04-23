using System;
using TTT.Core.Contracts.Enums;
using TTT.Core.Contracts.Interfaces.Menu.Items;

namespace TTT.Core.Contracts.Menu.Items
{
    public class UIMenuListItem : UIMenuItem
    {
        public UIMenuListItem(string text, string description, Action<IUIMenuItem, string> callback)
            : base(text, description, callback)
        {
            this.ItemData.ItemType = MenuItemType.ListItem;
        }
    }
}