import * as alt from 'alt-client';
import * as native from 'natives';
import Browser from '../WebView/Browser';
export default class Utils {
    static setPedIntoVehicle(veh) {
        native.setPedIntoVehicle(alt.Player.local, veh.scriptID, -1);
    }
    static setVehicleEngineMultiplier(multiplier) {
        let veh = alt.Player.local.vehicle;
        veh.handling.acceleration *= multiplier;
    }
    static toggleGameControls(state, showCursor = true) {
        alt.toggleGameControls(state);
        Browser.showCursor(showCursor);
    }
    static getClosestVehicle(player, range = 100) {
        if (!player || !player.valid) {
            return [undefined, undefined];
        }
        let vehicle;
        let _distance;
        alt.Vehicle.streamedIn.forEach(_vehicle => {
            const distance = player.pos.distanceTo(_vehicle.pos);
            if (distance <= (_distance ?? range)) {
                _distance = distance;
                vehicle = _vehicle;
            }
        });
        return [vehicle, _distance];
    }
    static distanceOf(vec1, vec2) {
        return Math.sqrt(Math.pow((vec1.x - vec2.x), 2) + Math.pow((vec1.y - vec2.y), 2) + Math.pow((vec1.z - vec2.z), 2));
    }
    static drawText3d(msg, position, scale, fontType, rgba, useOutline = true, useDropShadow = true, layer = 0) {
        native.setDrawOrigin(position.x, position.y, position.z, 0);
        native.beginTextCommandDisplayText('STRING');
        native.addTextComponentSubstringPlayerName(msg);
        native.setTextFont(fontType);
        native.setTextScale(1, scale);
        native.setTextWrap(0.0, 1.0);
        native.setTextCentre(true);
        native.setTextColour(rgba.r, rgba.g, rgba.b, rgba.a);
        if (useOutline)
            native.setTextOutline();
        if (useDropShadow)
            native.setTextDropShadow();
        native.endTextCommandDisplayText(0, 0, 0);
        native.clearDrawOrigin();
    }
    static entityDrawText3d(entity, msg, scale, fontType, rgba, useOutline = true, useDropShadow = true, isTME = false) {
        const playerPos = alt.Player.local.pos;
        const vector = native.getEntityVelocity(entity);
        const frameTime = native.getFrameTime();
        native.setDrawOrigin(playerPos.x + (vector.x * frameTime), playerPos.y + (vector.y * frameTime), playerPos.z + ((vector.z * frameTime) + (isTME ? 0.90 : 1.1)), 0);
        native.beginTextCommandDisplayText('STRING');
        native.addTextComponentSubstringPlayerName(msg);
        native.setTextFont(fontType);
        native.setTextScale(1, scale);
        native.setTextWrap(0.0, 1.0);
        native.setTextCentre(true);
        native.setTextColour(rgba.r, rgba.g, rgba.b, rgba.a);
        if (useOutline)
            native.setTextOutline();
        if (useDropShadow)
            native.setTextDropShadow();
        native.endTextCommandDisplayText(0, 0, 0);
        native.clearDrawOrigin();
    }
}
