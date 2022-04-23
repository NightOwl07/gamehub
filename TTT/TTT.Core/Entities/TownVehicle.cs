using System;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using TTT.Core.Contracts.Interfaces.Entities;
using TTT.EntitySync;

namespace TTT.Core.Entities
{
    public class TownVehicle : Vehicle, ITownVehicle
    {
        public TownVehicle(IServer server, IntPtr nativePointer, ushort id) : base(server, nativePointer, id)
        {
        }

        public string OwnerId { get; set; }

        public PlayerLabel Label { get; set; }

        public bool Locked { get; set; }

        public double CurrentFuel { get; set; }

        public double MaxFuel { get; set; }

        public ITownVehicle ToAsync(IAsyncContext asyncContext)
        {
            return new Async(this, asyncContext);
        }

        private class Async : AsyncVehicle<ITownVehicle>, ITownVehicle
        {
            public Async(ITownVehicle vehicle, IAsyncContext asyncContext) : base(vehicle, asyncContext)
            {
            }

            public string OwnerId
            {
                get => this.BaseObject.OwnerId;
                set => this.BaseObject.OwnerId = value;
            }

            public PlayerLabel Label
            {
                get => this.BaseObject.Label;
                set => this.BaseObject.Label = value;
            }

            public bool Locked
            {
                get => this.BaseObject.Locked;
                set => this.BaseObject.Locked = value;
            }

            public double CurrentFuel
            {
                get => this.BaseObject.CurrentFuel;
                set => this.BaseObject.CurrentFuel = value;
            }

            public double MaxFuel
            {
                get => this.BaseObject.MaxFuel;
                set => this.BaseObject.MaxFuel = value;
            }

            public ITownVehicle ToAsync(IAsyncContext asyncContext)
            {
                return asyncContext == this.AsyncContext ? this : new Async(this.BaseObject, asyncContext);
            }
        }
    }
}