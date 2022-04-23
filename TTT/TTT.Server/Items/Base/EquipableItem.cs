using AltV.Net.Elements.Entities;
using TTT.Server.Contracts.Interfaces.Items;

namespace TTT.Server.Items.Base
{
    public abstract class EquipableItem : Item, IEquipableItem
    {
        protected virtual void OnEquip(IPlayer player)
        {
        }
    }
}