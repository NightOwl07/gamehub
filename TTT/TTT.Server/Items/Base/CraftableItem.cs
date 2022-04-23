using AltV.Net.Elements.Entities;
using TTT.Server.Contracts.Interfaces.Items;

namespace TTT.Server.Items.Base
{
    public abstract class CraftableItem : Item, ICraftableItem
    {
        protected virtual void OnCraft(IPlayer player)
        {
        }
    }
}