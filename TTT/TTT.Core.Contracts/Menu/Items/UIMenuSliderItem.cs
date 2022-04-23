using System;
using TTT.Core.Contracts.Enums;
using TTT.Core.Contracts.Interfaces.Menu.Items;

namespace TTT.Core.Contracts.Menu.Items
{
    public class UIMenuSliderItem : UIMenuItem
    {
        public UIMenuSliderItem(string text, string description, Action<IUIMenuItem, string> callback = null) : base(
            text, description, callback)
        {
            this.ItemData.ItemType = MenuItemType.SliderItem;
        }
    }
}