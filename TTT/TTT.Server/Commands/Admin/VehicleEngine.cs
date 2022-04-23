using AltV.Net.Resources.Chat.Api;
using CustomCommandsSystem.Common.Attributes;
using TTT.Core.Entities;

namespace TTT.Server.Commands.Admin
{
    public class VehicleEngine
    {
        [CustomCommand("vengine")]
        [CustomCommandAlias("vehengine")]
        public void SetVehicleEngineMultiplier(TownPlayer player, int multiplier)
        {
            if (player.Vehicle == null) player?.SendChatMessage("Du bist in keinem Fahrzeug!");

            player.Emit("TTT:Utils:SetVehicleEngineMultiplier", multiplier);
        }
    }
}