using System;
using AltV.Net.Elements.Entities;
using TTT.Server.Items.Base;

namespace TTT.Server.Items
{
    public class FirstAidKit : UsableItem
    {
        public FirstAidKit(int inventoryId) : base(inventoryId)
        {
        }

        public override string Name => "Erste-Hilfe-Kasten";

        public override string Description => "Kann benutzt werden um sich zu heilen.";

        protected override void OnUse(IPlayer player)
        {
            Console.WriteLine("Player used first aid kid");

            player.Health = 100;
        }
    }
}