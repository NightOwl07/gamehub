using System;
using Newtonsoft.Json;
using TTT.Core.Contracts.Enums;
using TTT.Core.Contracts.Interfaces.Menu.Items;

namespace TTT.Core.Contracts.Menu.Items
{
    public class UIMenuItem : IUIMenuItem
    {
        public UIMenuItem(string text, string description, Action<IUIMenuItem, string> callback = null)
        {
            this.Text = text;
            this.Description = description;
            this.Callback = callback;

            this.ItemData = new MenuItemData
            {
                Id = Guid.NewGuid(),
                ItemType = MenuItemType.Item
            };
        }

        public UIMenuItem(string text, string description, string rightLabel,
            Action<IUIMenuItem, string> callback = null)
            : this(text, description, callback)
        {
            this.RightLabel = rightLabel;
        }

        public string Text { get; }

        public string Description { get; }

        public string RightLabel { get; }

        [JsonIgnore] private Action<IUIMenuItem, string> Callback { get; }

        public IMenuItemData ItemData { get; }

        public void ExecuteAction(string result)
        {
            this.Callback?.Invoke(this, result);
        }
    }
}