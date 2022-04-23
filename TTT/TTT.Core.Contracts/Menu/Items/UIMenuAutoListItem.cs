using System;
using TTT.Core.Contracts.Enums;
using TTT.Core.Contracts.Interfaces.Menu.Items;

namespace TTT.Core.Contracts.Menu.Items
{
    public class UIMenuAutoListItem : UIMenuItem
    {
        public UIMenuAutoListItem(string text, string description, int lowerThreshold, int upperThreshold,
            int startValue, Action<IUIMenuItem, string> callback = null)
            : base(text, description, callback)
        {
            this.ItemData.ItemType = MenuItemType.AutoListItem;
        }
    }
}