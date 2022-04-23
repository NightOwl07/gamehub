using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace TTT.Core.Entities.Factories
{
    public class TownPlayerFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(IServer server, IntPtr entityPointer, ushort id)
        {
            return new TownPlayer(server, entityPointer, id);
        }
    }
}