using System;
using AltV.Net.Elements.Entities;
using TTT.Server.Contracts.Interfaces.Items;

namespace TTT.Server.Items.Base
{
    public abstract class Item : IItem
    {
        protected Item()
        {
            this.Validate();
        }

        protected Item(int inventoryId)
        {
            this.InventoryId = inventoryId;
            this.Quantity = 1;
        }

        protected Item(int inventoryId, int quantity)
        {
            this.InventoryId = inventoryId;
            this.Quantity = quantity;
        }

        public int Quantity { get; set; }
        public abstract string Name { get; }

        public abstract string Description { get; }

        public int InventoryId { get; set; }

        protected virtual void OnInteract(IPlayer player)
        {
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(this.Name))
                throw new NotImplementedException($"Property \"{nameof(this.Name)}\" is not implemented correctly!");

            if (this.InventoryId <= 0) throw new NotSupportedException("Inventory ID is <= 0!");
        }
    }
}