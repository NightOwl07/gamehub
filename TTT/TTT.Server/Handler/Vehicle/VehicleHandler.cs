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
            vehicle.EngineOn = !vehicle.EngineOn;
        }

        private async Task OnPlayerLeaveVehicle(ITownVehicle vehicle, ITownPlayer player, byte seat)
        {
        }

        private async Task OnLockVehicle(ITownPlayer player, ITownVehicle vehicle)
        {
            vehicle.Locked = !vehicle.Locked;
            vehicle.LockState = vehicle.Locked ? VehicleLockState.Locked : VehicleLockState.Unlocked;
            vehicle.SetStreamSyncedMetaData("Locked", vehicle.Locked);

            Utils.Utils.EmitInRange("TTT:Client:VehicleHandler:LockVehicle", vehicle.Position, 100,
                vehicle, vehicle.Locked);
        }
    }
}