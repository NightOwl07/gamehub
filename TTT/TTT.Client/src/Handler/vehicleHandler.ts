import * as alt from 'alt-client';
import native from "natives";
import Utils from '../Utils/utils';
import VehicleFuelHandler from './vehicle/vehicleFuelHandler';

export default class VehicleHandler {
    private coolDown: Date;

    constructor() {
        this.coolDown = new Date();

        new VehicleFuelHandler();

        alt.on("keydown", this.onKeyDown.bind(this));
        alt.on("leftVehicle", this.onLeftVehicle.bind(this));
        alt.on("enteredVehicle", this.onEnteredVehicle.bind(this));
        alt.onServer("TTT:Client:VehicleHandler:PlayerEnteringVehicle", this.onPlayerEnteringVehicle.bind(this));
        alt.onServer("TTT:Client:VehicleHandler:LockVehicle", this.onLockVehicle.bind(this));
    }

    private onKeyDown(key: number): void {
        switch (key) {
            case 76: // L
                this.tryLockVehicle();
                break;
            case 88: // X
                this.toggleVehicleEngine();
            default:
                break;
        }
    }

    public toggleVehicleEngine(): void {
        let veh: alt.Vehicle = alt.Player.local.vehicle;
        
        if(!veh || alt.Player.local.seat !== 1) {
            return;
        }

        alt.emitServer("TTT:VehicleHandler:ToggleVehicleEngine", alt.Player.local.vehicle);
    }

    public onLeftVehicle(vehicle: alt.Vehicle): void {
        native.displayRadar(false);
    }

    public onEnteredVehicle(vehicle: alt.Vehicle, seat: number): void {
        native.displayRadar(true);

        // TODO: Do this check serverside
        if (vehicle?.getStreamSyncedMeta("OwnerId") != alt.Player.local.getStreamSyncedMeta("Id")) {
            alt.Player.local.sendNotification(1, "Info", "Du kannst dieses Auto auch kurzschließen!", 4500);
        }
    }

    public onPlayerEnteringVehicle(vehicle: alt.Vehicle): void {
        if (!native.isVehicleDriveable(vehicle.scriptID, false))
            return;

        if(vehicle?.getStreamSyncedMeta("Locked")) {
            return;
        }
    
      native.taskEnterVehicle(alt.Player.local.scriptID, vehicle.scriptID, 10_000, 1, 1, 1, 0);
    }

    private tryLockVehicle() {
        if (!alt.Player.local.canInteract(this.coolDown)) {
            return;
        }

        let vehicle: alt.Vehicle = Utils.getClosestVehicle(alt.Player.local)[0];
        // TODO: Do this check serverside
        if (vehicle?.getStreamSyncedMeta("OwnerId") == alt.Player.local.getStreamSyncedMeta("Id")) {
            alt.emitServer("TTT:VehicleHandler:LockVehicle", vehicle);
        }

        this.coolDown = new Date();
    }

    private onLockVehicle(vehicle: alt.Vehicle, state: boolean) { // state true == locked
        let vehId: number = vehicle.scriptID;

        if (state) {
            native.setVehicleDoorsLocked(vehId, 2);
            native.setVehicleLights(vehId, 2);
            native.playVehicleDoorCloseSound(vehId, 0);
            alt.setTimeout(() => {
                native.setVehicleLights(vehId, 1);
                alt.setTimeout(() => {
                    native.setVehicleLights(vehId, 2);
                    alt.setTimeout(() => {
                        native.setVehicleLights(vehId, 0);
                    }, 100);
                }, 100);
            }, 100);
        }
        else {
            native.setVehicleDoorsLocked(vehId, 1);
            native.setVehicleLights(vehId, 2);
            native.playVehicleDoorOpenSound(vehId, 0);
            alt.setTimeout(() => {
                native.setVehicleLights(vehId, 1);
                alt.setTimeout(() => {
                    native.setVehicleLights(vehId, 2);
                    alt.setTimeout(() => {
                        native.setVehicleLights(vehId, 0);
                    }, 100);
                }, 100);
            }, 100);
        }
    }
}