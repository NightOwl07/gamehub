/// <reference types="types-client" />
import * as alt from 'alt-client';
export default class VehicleHandler {
    private coolDown;
    constructor();
    private onKeyDown;
    toggleVehicleEngine(): void;
    onLeftVehicle(vehicle: alt.Vehicle): void;
    onEnteredVehicle(vehicle: alt.Vehicle, seat: number): void;
    onPlayerEnteringVehicle(vehicle: alt.Vehicle): void;
    private tryLockVehicle;
    private onLockVehicle;
}
