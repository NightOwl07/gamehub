using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using CustomCommandsSystem.Common.Attributes;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Contracts.Interfaces.Entities;
using TTT.Server.Contracts.Interfaces.Services;
using TTT.Core.Entities;

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
            TownPlayer player1 = (TownPlayer)player;

            if (Utils.Utils.CheckAdmin(player1, TTT.Contracts.Base.Enums.PermissionLevel.Administration))
            {
                await using IAsyncContext asyncContext = AsyncContext.Create();

                if (!player.TryToAsync(asyncContext, out ITownPlayer asyncPlayer)) return;

                ITownVehicle veh =
                    await AltAsync.Do(() => Alt.CreateVehicle(Alt.Hash(model), asyncPlayer.Position, new Rotation())) as
                        ITownVehicle;

                if (veh == null)
                {
                    this._notificationService.SendErrorNotification(asyncPlayer, "Fehler!",
                        "Fehler beim erstellen des Fahrzeugs!");
                    return;
                }

                if (!veh.TryToAsync(asyncContext, out ITownVehicle asyncVehicle)) return;

                asyncVehicle.OwnerId = charId == 0 ? asyncPlayer.Account.Id.ToString() : charId.ToString(); // TODO: use character id if available
                asyncVehicle.NumberplateText = "TTT BOSS";
                asyncVehicle.SetStreamSyncedMetaData("OwnerId", asyncVehicle.OwnerId.ToString());

                await Task.Delay(450);

                await asyncPlayer.EmitAsync("TTT:Utils:SetPedIntoVehicle", veh);
            }
        }
    }
}