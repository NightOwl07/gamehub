using System.Linq;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace TTT.Server.Utils
{
    public static class Utils
    {
        public static void EmitInRange(string eventName, Position pos, float range, params object[] args)
        {
            foreach (IPlayer player in Alt.GetAllPlayers().Where(p => p.Exists && p.Position.Distance(pos) <= range))
                player.EmitLocked(eventName, args);
        }
    }
}