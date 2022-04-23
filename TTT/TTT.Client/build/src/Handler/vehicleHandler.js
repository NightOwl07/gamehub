import * as alt from 'alt-client';
import native from "natives";
import Utils from '../Utils/utils';
export default class VehicleHandler {
    coolDown;
    constructor() {
        this.coolDown = new Date();
        alt.on("keydown", this.onKeyDown.bind(this));
        alt.on("leftVehicle", this.onLeftVehicle.bind(this));
        alt.on("enteredVehicle", this.onEnteredVehicle.bind(this));
        alt.onServer("TTT:Client:VehicleHandler:PlayerEnteringVehicle", this.onPlayerEnteringVehicle.bind(this));
        alt.onServer("TTT:Client:VehicleHandler:LockVehicle", this.onLockVehicle.bind(this));
    }
    onKeyDown(key) {
        switch (key) {
            case 76:
                this.tryLockVehicle();
                break;
            case 88:
                this.toggleVehicleEngine();
            default:
                break;
        }
    }
    toggleVehicleEngine() {
        let veh = alt.Player.local.vehicle;
        if (!veh || alt.Player.local.seat !== 1) {
            return;
        }
        alt.emitServer("TTT:VehicleHandler:ToggleVehicleEngine", alt.Player.local.vehicle);
    }
    onLeftVehicle(vehicle) {
        native.displayRadar(false);
    }
    onEnteredVehicle(vehicle, seat) {
        native.displayRadar(true);
        if (vehicle?.getStreamSyncedMeta("OwnerId") != alt.Player.local.getStreamSyncedMeta("Id")) {
            alt.Player.local.sendNotification(1, "Info", "Du kannst dieses auch kurzschließen!", 4500);
        }
    }
    onPlayerEnteringVehicle(vehicle) {
        if (!native.isVehicleDriveable(vehicle.scriptID, false))
            return;
        alt.log("player entering vehicle");
        if (vehicle?.getStreamSyncedMeta("Locked")) {
            alt.log("vehicle locked");
            return;
        }
        native.taskEnterVehicle(alt.Player.local.scriptID, vehicle.scriptID, 10_000, 1, 1, 1, 0);
    }
    tryLockVehicle() {
        if (!alt.Player.local.canInteract(this.coolDown)) {
            return;
        }
        let vehicle = Utils.getClosestVehicle(alt.Player.local)[0];
        if (vehicle?.getStreamSyncedMeta("OwnerId") == alt.Player.local.getStreamSyncedMeta("Id")) {
            alt.emitServer("TTT:VehicleHandler:LockVehicle", vehicle);
        }
        this.coolDown = new Date();
    }
    onLockVehicle(vehicle, state) {
        let vehId = vehicle.scriptID;
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
