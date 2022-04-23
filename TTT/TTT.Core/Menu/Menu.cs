using System;
using System.Collections.Generic;
using System.Drawing;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json;
using TTT.Core.Contracts.Interfaces.Menu;
using TTT.Core.Contracts.Interfaces.Menu.Items;
using TTT.Core.Contracts.Menu.Items;
using TTT.Core.Entities;

namespace TTT.Core.Menu
{
    public class Menu : IMenu
    {
        public Menu(string title, string subTitle, Point offset, string spriteLibrary = "", string spriteName = "")
        {
            this.Id = Guid.NewGuid();

            this.Title = title;
            this.SubTitle = subTitle;
            this.Offset = offset;
            this.SpriteLibrary = spriteLibrary;
            this.SpriteName = spriteName;

            this.Items = new List<IUIMenuItem>();
        }

        public List<IUIMenuItem> Items { get; set; }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public Point Offset { get; set; }

        public string SpriteLibrary { get; set; }

        public string SpriteName { get; set; }

        public void Show(IPlayer player)
        {
            ((TownPlayer)player).ActiveMenu = this;
            player?.Emit("TTT:MenuHandler:CreateNewMenu", JsonConvert.SerializeObject(this));
        }

        public void Hide(IPlayer player)
        {
            player?.Emit("TTT:MenuHandler:DestroyMenu");
        }

        public IMenu AddAutoListItem(string text, string description, int lowerThreshold, int upperThreshold,
            int startValue, Action<IUIMenuItem, string> callback = null)
        {
            return this.AddItem(new UIMenuAutoListItem(text, description, lowerThreshold, upperThreshold, startValue,
                callback));
        }

        public IMenu AddCheckboxItem(string text, bool check, string description = "",
            Action<IUIMenuItem, string> callback = null)
        {
            return this.AddItem(new UIMenuCheckboxItem(text, check, description, callback));
        }

        public IMenu AddDynamicListItem<T>(string text, string description, IEnumerable<T> collectionData,
            Action<IUIMenuItem, string> callback = null)
        {
            return this.AddItem(new UIMenuDynamicListItem<T>(text, description, collectionData, callback));
        }

        public IMenu AddItem(string text, string description, Action<IUIMenuItem, string> callback = null)
        {
            return this.AddItem(new UIMenuItem(text, description, callback));
        }

        public IMenu AddItem(string text, string description, string rightLabel,
            Action<IUIMenuItem, string> callback = null)
        {
            return this.AddItem(new UIMenuItem(text, description, rightLabel, callback));
        }

        public IMenu AddListItem(string text, string description, Action<IUIMenuItem, string> callback)
        {
            return this.AddItem(new UIMenuListItem(text, description, callback));
        }

        public IMenu AddSliderItem(string text, string description, Action<IUIMenuItem, string> callback = null)
        {
            return this.AddItem(new UIMenuSliderItem(text, description, callback));
        }

        private IMenu AddItem(IUIMenuItem item)
        {
            this.Items.Add(item);
            return this;
        }
    }
}