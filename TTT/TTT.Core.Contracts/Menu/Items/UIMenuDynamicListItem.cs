using System;
using System.Collections.Generic;
using TTT.Core.Contracts.Enums;
using TTT.Core.Contracts.Interfaces.Menu.Items;

namespace TTT.Core.Contracts.Menu.Items
{
    public class UIMenuDynamicListItem<T> : UIMenuItem
    {
        public IEnumerable<T> CollectionData;

        public UIMenuDynamicListItem(string text, string description, IEnumerable<T> collectionData,
            Action<IUIMenuItem, string> callback = null)
            : base(text, description, callback)
        {
            if (typeof(T) != typeof(string) && !typeof(T).IsValueType)
                throw new ArgumentException("u fucked up, haha!");

            if (typeof(T).IsClass && typeof(T) != typeof(string)) throw new ArgumentException("u fucked up, haha!");

            this.CollectionData = collectionData ?? new List<T>();
            this.ItemData.ItemType = MenuItemType.DynamicListItem;
        }
    }
}