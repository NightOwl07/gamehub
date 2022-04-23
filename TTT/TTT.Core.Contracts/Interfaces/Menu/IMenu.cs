using System;
using System.Collections.Generic;
using System.Drawing;
using AltV.Net.Elements.Entities;
using TTT.Core.Contracts.Interfaces.Menu.Items;

namespace TTT.Core.Contracts.Interfaces.Menu
{
    public interface IMenu
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public Point Offset { get; set; }

        public string SpriteLibrary { get; set; }

        public string SpriteName { get; set; }

        public IMenu AddAutoListItem(string text, string description, int lowerThreshold, int upperThreshold,
            int startValue, Action<IUIMenuItem, string> callback = null);

        public IMenu AddCheckboxItem(string text, bool check, string description = "",
            Action<IUIMenuItem, string> callback = null);

        public IMenu AddDynamicListItem<T>(string text, string description, IEnumerable<T> collectionData,
            Action<IUIMenuItem, string> callback = null);

        public IMenu AddItem(string text, string description, Action<IUIMenuItem, string> callback = null);

        public IMenu AddItem(string text, string description, string rightLabel,
            Action<IUIMenuItem, string> callback = null);

        public IMenu AddListItem(string text, string description, Action<IUIMenuItem, string> callback);

        public IMenu AddSliderItem(string text, string description, Action<IUIMenuItem, string> callback = null);

        void Show(IPlayer player);

        void Hide(IPlayer player);
    }
}