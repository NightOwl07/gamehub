using System;
using TTT.Core.Contracts.Enums;

namespace TTT.Core.Contracts.Interfaces.Menu.Items
{
    public interface IMenuItemData
    {
        Guid Id { get; set; }

        MenuItemType ItemType { get; set; }
    }
}