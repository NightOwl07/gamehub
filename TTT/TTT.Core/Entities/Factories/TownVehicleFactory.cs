using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace TTT.Core.Entities.Factories
{
    public class TownVehicleFactory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(ICore core, IntPtr vehiclePointer, ushort id)
        {
            return new TownVehicle(core, vehiclePointer, id);
        }
    }
}