import * as alt from 'alt-client';
import * as native from 'natives';
import { Vector3 } from "alt-client";
const disabledControls = [30, 31, 21, 36, 22, 44, 38, 71, 72, 59, 60, 43, 85, 86, 15, 14, 228, 229, 37, 44, 178, 244, 220, 221, 218, 219, 16, 17];
export class FlyCam {
    static _instance;
    _camId = null;
    _tickId = null;
    _maxSpeed = 50.00;
    _minSpeed = 0.10;
    _speed = 1.50;
    isActive = false;
    _currentRot;
    constructor() {
        if (FlyCam._instance)
            return;
    }
    static getInstance() {
        if (!FlyCam._instance)
            FlyCam._instance = new FlyCam();
        return FlyCam._instance;
    }
    toggle() {
        if (this.isActive) {
            native.resetEntityAlpha(alt.Player.local.scriptID);
            if (this._tickId != null) {
                alt.clearEveryTick(this._tickId);
                this._tickId = null;
            }
            if (this._camId != null) {
                native.renderScriptCams(false, false, 0, true, false, 0);
                native.setCamActive(this._camId, false);
                native.destroyCam(this._camId, false);
                this._camId = null;
            }
            let [_, groundZ] = native.getGroundZFor3dCoord(alt.Player.local.pos.x, alt.Player.local.pos.y, alt.Player.local.pos.z, alt.Player.local.pos.z + 1, true, true);
            native.setEntityCoords(alt.Player.local.scriptID, alt.Player.local.pos.x, alt.Player.local.pos.y, groundZ + 1, false, false, false, false);
            native.freezeEntityPosition(alt.Player.local.scriptID, false);
            this.isActive = false;
        }
        else {
            native.setEntityAlpha(alt.Player.local.scriptID, 0, false);
            native.freezeEntityPosition(alt.Player.local.scriptID, true);
            this._tickId = alt.everyTick(this.handleCamera.bind(this));
            this.isActive = true;
        }
    }
    handleCamera() {
        if (native.isPauseMenuActive())
            return;
        if (this._camId == null) {
            native.destroyAllCams(true);
            this._camId = native.createCamWithParams("DEFAULT_SCRIPTED_CAMERA", alt.Player.local.pos.x, alt.Player.local.pos.y, alt.Player.local.pos.z, 0, 0, 0, 90, undefined, 2);
            native.setCamActive(this._camId, true);
            native.renderScriptCams(true, false, 0, true, false, 0);
            native.setCamAffectsAiming(this._camId, false);
        }
        disabledControls.forEach((ctrl) => {
            native.disableControlAction(0, ctrl, true);
        });
        native.disableFirstPersonCamThisFrame();
        native.blockWeaponWheelThisFrame();
        if (native.isDisabledControlJustPressed(0, 14)) {
            this._speed *= 1.25;
            this._speed = parseFloat(this._speed.toFixed(2));
            if (this._speed > this._maxSpeed)
                this._speed = this._maxSpeed;
        }
        if (native.isDisabledControlJustPressed(0, 15)) {
            this._speed *= 0.75;
            this._speed = parseFloat(this._speed.toFixed(2));
            if (this._speed < this._minSpeed)
                this._speed = this._minSpeed;
        }
        const rightAxisX = native.getDisabledControlNormal(0, 220);
        const rightAxisY = native.getDisabledControlNormal(0, 221);
        const leftAxisX = native.getDisabledControlNormal(0, 218);
        const leftAxisY = native.getDisabledControlNormal(0, 219);
        const effectiveSpeed = native.isDisabledControlPressed(0, 21) ? this._speed * 2 : this._speed;
        const pos = native.getCamCoord(this._camId);
        const rot = native.getCamRot(this._camId, 2);
        const rr = this.rotationToDirection(rot);
        const preRightVector = this.getCrossProduct(this.getNormalizedVector(rr), this.getNormalizedVector(new Vector3(0, 0, 1)));
        const movementVector = new alt.Vector3(rr.x * leftAxisY * effectiveSpeed, rr.y * leftAxisY * effectiveSpeed, rr.z * leftAxisY * (effectiveSpeed / 1.5));
        const rightVector = new alt.Vector3(preRightVector.x * leftAxisX * effectiveSpeed, preRightVector.y * leftAxisX * effectiveSpeed, preRightVector.z * leftAxisX * effectiveSpeed);
        const newPos = new alt.Vector3(pos.x - movementVector.x + rightVector.x, pos.y - movementVector.y + rightVector.y, pos.z - movementVector.z + rightVector.z + ((native.isDisabledControlPressed(0, 38)) ? this._speed : 0) - ((native.isDisabledControlPressed(0, 44)) ? this._speed : 0));
        this._currentRot = new Vector3(this.clamp(rot.x + rightAxisY * -5.0, -90, 90), 0.0, rot.z + rightAxisX * -5.0);
        native.setCamCoord(this._camId, newPos.x, newPos.y, newPos.z);
        native.setCamRot(this._camId, this.clamp(rot.x + rightAxisY * -5.0, -90, 90), 0.0, rot.z + rightAxisX * -5.0, 2);
        native.setEntityCoords(alt.Player.local.scriptID, newPos.x, newPos.y, newPos.z - 1, false, false, false, false);
        let oldHeading = native.getEntityHeading(alt.Player.local.scriptID);
        native.setEntityRotation(alt.Player.local.scriptID, 0, 0, rot.z + rightAxisY, 2, true);
        native.setGameplayCamRelativeHeading(oldHeading - native.getEntityHeading(alt.Player.local.scriptID));
    }
    clamp(value, min, max) {
        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
    }
    rotationToDirection(rotation) {
        const z = (rotation.z * Math.PI) / 180;
        const x = (rotation.x * Math.PI) / 180;
        const num = Math.abs(Math.cos(x));
        return new alt.Vector3(-Math.sin(z) * num, Math.cos(z) * num, Math.sin(x));
    }
    getNormalizedVector(v1) {
        const mag = Math.sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z);
        return new alt.Vector3(v1.x / mag, v1.y / mag, v1.z / mag);
    }
    getCrossProduct(v1, v2) {
        return new alt.Vector3(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
    }
    getInfo() {
        alt.log(native.getCamCoord(this._camId));
        alt.log(this._currentRot);
    }
}
