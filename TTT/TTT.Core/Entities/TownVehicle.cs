using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using System;
using TTT.Core.Contracts.Interfaces.Entities;
using TTT.EntitySync;

namespace TTT.Core.Entities
{
    public class TownVehicle : AsyncVehicle, ITownVehicle
    {
        public TownVehicle(ICore core, IntPtr nativePointer, ushort id) : base(core, nativePointer, id)
        {
        }

        public string OwnerId { get; set; }

        public PlayerLabel Label { get; set; }

        public bool Locked { get; set; }

        public double CurrentFuel { get; set; }

        public double MaxFuel { get; set; }
    }
}