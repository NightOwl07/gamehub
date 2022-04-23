using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace TTT.Core.Entities.Factories
{
    public class TownColShapeFactory : IBaseObjectFactory<IColShape>
    {
        public IColShape Create(IServer server, IntPtr colShapePointer)
        {
            return new TownColShape(server, colShapePointer);
        }
    }
}