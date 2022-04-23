using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace TTT.Core.Entities.Factories
{
    public class TownVehicleFactory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(IServer server, IntPtr vehiclePointer, ushort id)
        {
            return new TownVehicle(server, vehiclePointer, id);
        }
    }
}