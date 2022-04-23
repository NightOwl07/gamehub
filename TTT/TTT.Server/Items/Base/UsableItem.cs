using AltV.Net.Elements.Entities;
using TTT.Server.Contracts.Interfaces.Items;

namespace TTT.Server.Items.Base
{
    public abstract class UsableItem : Item, IUsableItem
    {
        protected UsableItem(int inventoryId) : base(inventoryId)
        {
        }

        protected UsableItem(int inventoryId, int quantity) : base(inventoryId, quantity)
        {
        }

        protected virtual void OnUse(IPlayer player)
        {
        }
    }
}