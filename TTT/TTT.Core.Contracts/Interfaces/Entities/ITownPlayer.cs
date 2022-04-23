using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using TTT.Database.Contracts.Models;

namespace TTT.Core.Contracts.Interfaces.Entities
{
    public interface ITownPlayer : IPlayer, IAsyncConvertible<ITownPlayer>
    {
        Account Account { get; set; }

        Character Character { get; set; }
    }
}