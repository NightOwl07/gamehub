using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Enums;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Contracts.Interfaces.Entities;
using TTT.Server.Contracts.Interfaces.Services;

namespace TTT.Server.Handler.Vehicle
{
    public class VehicleHandler : ISingletonScript
    {
        private readonly ILogger<VehicleHandler> _logger;
        private readonly INotificationService _notificationService;

        public VehicleHandler(INotificationService notificationService, ILogger<VehicleHandler> logger)
        {
            this._logger = logger;
            this._notificationService = notificationService;

            Alt.OnPlayerLeaveVehicle += (vehicle, player, seat) =>
                Task.Run(() => this.OnPlayerLeaveVehicle(vehicle as ITownVehicle, player as ITownPlayer, seat));
            AltAsync.OnClient<ITownPlayer, ITownVehicle>("TTT:VehicleHandler:LockVehicle", (player, vehicle) =>
                Task.Run(() => this.OnLockVehicle(player, vehicle)));
            AltAsync.OnClient<ITownPlayer, ITownVehicle>("TTT:VehicleHandler:ToggleVehicleEngine", (player, vehicle) =>
                Task.Run(() => this.OnToggleVehicleEngine(player, vehicle)));
        }

        private async Task OnToggleVehicleEngine(ITownPlayer player, ITownVehicle vehicle)
        {
            await using (IAsyncContext asyncContext = AsyncContext.Create())
            {
                if (!player.TryToAsync(asyncContext, out ITownPlayer asyncPlayer) ||
                    !vehicle.TryToAsync(asyncContext, out ITownVehicle asyncVehicle))
                    return;

                asyncVehicle.EngineOn = !asyncVehicle.EngineOn;
            }
        }

        private async Task OnPlayerLeaveVehicle(ITownVehicle vehicle, ITownPlayer player, byte seat)
        {
            await using (IAsyncContext asyncContext = AsyncContext.Create())
            {
                if (!player.TryToAsync(asyncContext, out ITownPlayer asyncPlayer) ||
                    !vehicle.TryToAsync(asyncContext, out ITownVehicle asyncVehicle))
                    return;
            }
        }

        private async Task OnLockVehicle(ITownPlayer player, ITownVehicle vehicle)
        {
            await using (IAsyncContext asyncContext = AsyncContext.Create())
            {
                if (!player.TryToAsync(asyncContext, out ITownPlayer asyncPlayer) ||
                    !vehicle.TryToAsync(asyncContext, out ITownVehicle asyncVehicle))
                    return;

                asyncVehicle.Locked = !asyncVehicle.Locked;
                asyncVehicle.LockState = asyncVehicle.Locked ? VehicleLockState.Locked : VehicleLockState.Unlocked;
                asyncVehicle.SetStreamSyncedMetaData("Locked", asyncVehicle.Locked);

                Utils.Utils.EmitInRange("TTT:Client:VehicleHandler:LockVehicle", asyncVehicle.Position, 100,
                    asyncVehicle, asyncVehicle.Locked);
            }
        }
    }
}