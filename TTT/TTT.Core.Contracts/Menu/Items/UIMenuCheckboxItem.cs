using System;
using TTT.Core.Contracts.Enums;
using TTT.Core.Contracts.Interfaces.Menu.Items;

namespace TTT.Core.Contracts.Menu.Items
{
    public class UIMenuCheckboxItem : UIMenuItem
    {
        public UIMenuCheckboxItem(string text, bool check, string description = "",
            Action<IUIMenuItem, string> callback = null)
            : base(text, description, callback)
        {
            this.Checked = check;
            this.ItemData.ItemType = MenuItemType.CheckboxItem;
        }

        public bool Checked { get; set; }
    }
}