using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace TTT.Core.Entities.Factories
{
    public class TownPlayerFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(ICore core, IntPtr entityPointer, ushort id)
        {
            return new TownPlayer(core, entityPointer, id);
        }
    }
}