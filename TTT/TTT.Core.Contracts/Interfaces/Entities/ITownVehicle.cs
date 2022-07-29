using AltV.Net.Elements.Entities;
using TTT.EntitySync;

namespace TTT.Core.Contracts.Interfaces.Entities
{
    public interface ITownVehicle : IVehicle
    {
        public string OwnerId { get; set; }

        public PlayerLabel Label { get; set; }

        public bool Locked { get; set; }

        public double CurrentFuel { get; set; }

        public double MaxFuel { get; set; }
    }
}