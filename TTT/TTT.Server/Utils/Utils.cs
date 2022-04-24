using System.Linq;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Entities;

namespace TTT.Server.Utils
{
    public static class Utils
    {
        public static void EmitInRange(string eventName, Position pos, float range, params object[] args)
        {
            foreach (IPlayer player in Alt.GetAllPlayers().Where(p => p.Exists && p.Position.Distance(pos) <= range))
                player.EmitLocked(eventName, args);
        }

        public static bool CheckAdmin(TownPlayer player, TTT.Contracts.Base.Enums.PermissionLevel adminlvl)
        {
            if (player.Account.PermissionLevel >= adminlvl)
            {
                return true;
            }
            player.SendChatMessage("Dein Adminlevel ist zu niedrig");
            return false;
        }
    }
}