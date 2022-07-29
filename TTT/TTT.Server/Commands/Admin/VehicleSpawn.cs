using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using CustomCommandsSystem.Common.Attributes;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Contracts.Interfaces.Entities;
using TTT.Server.Contracts.Interfaces.Services;

namespace TTT.Server.Commands.Admin
{
    public class VehicleSpawn : ISingletonCommand
    {
        private readonly INotificationService _notificationService;

        public VehicleSpawn(INotificationService notificationService)
        {
            this._notificationService = notificationService;
        }

        [CustomCommand("veh")]
        [CustomCommandAlias("spawnveh")]
        public async Task SpawnVehicle(ITownPlayer player, string model, int charId = 0)
        {
            ITownVehicle veh =
                await AltAsync.Do(() => Alt.CreateVehicle(Alt.Hash(model), player.Position, new Rotation())) as
                    ITownVehicle;

            if (veh == null)
            {
                this._notificationService.SendErrorNotification(player, "Fehler!",
                    "Fehler beim erstellen des Fahrzeugs!");
                return;
            }
            
            veh.OwnerId = charId == 0 ? player.Account.Id.ToString() : charId.ToString(); // TODO: use character id if available
            veh.NumberplateText = "TTT BOSS";
            veh.SetStreamSyncedMetaData("OwnerId", veh.OwnerId.ToString());

            await Task.Delay(450);

            await player.EmitAsync("TTT:Utils:SetPedIntoVehicle", veh);
        }
    }
}