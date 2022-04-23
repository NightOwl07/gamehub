using AltV.Net.Elements.Entities;
using TTT.Server.Contracts.Interfaces.Items;

namespace TTT.Server.Items.Base
{
    public abstract class ConsumableItem : Item, IConsumableItem
    {
        protected virtual void OnCosume(IPlayer player)
        {
        }
    }
}