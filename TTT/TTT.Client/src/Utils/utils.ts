import * as alt from 'alt-client';
import * as native from 'natives';
import Browser from '../WebView/Browser';

export default abstract class Utils {
    public static setPedIntoVehicle(veh: alt.Vehicle): void {
        native.setPedIntoVehicle(alt.Player.local, veh.scriptID, -1);
    }

    public static setVehicleEngineMultiplier(multiplier: number): void {
        let veh: alt.Vehicle = alt.Player.local.vehicle;

        veh.handling.acceleration *= multiplier;
    }

    public static toggleGameControls(state: boolean, showCursor: boolean = true): void {
        alt.toggleGameControls(state);
        Browser.showCursor(showCursor);
    }

    public static getClosestVehicle(player: alt.Player, range: number = 100): [alt.Vehicle | undefined, number | undefined] {
        if (!player || !player.valid) {
            return [undefined, undefined];
        }

        let vehicle: alt.Vehicle | undefined;
        let _distance: number | undefined;

        // can also use streamed in vehicles only (using `gameEntityCreate` and `gameEntityRemove` events)
        alt.Vehicle.streamedIn.forEach(_vehicle => {
            const distance = player.pos.distanceTo(_vehicle.pos);

            if (distance <= (_distance ?? range)) {
                _distance = distance;
                vehicle = _vehicle;
            }
        });

        return [vehicle, _distance];
    }

    public static distanceOf(vec1: alt.Vector3, vec2: alt.Vector3): number {
        return Math.sqrt(Math.pow((vec1.x - vec2.x), 2) + Math.pow((vec1.y - vec2.y), 2) + Math.pow((vec1.z - vec2.z), 2));
    }

    public static drawText3d(msg: string, position: alt.Vector3, scale: number, fontType: number, rgba: alt.RGBA, useOutline = true, useDropShadow = true, layer = 0) {
        native.setDrawOrigin(position.x, position.y, position.z, 0);
        native.beginTextCommandDisplayText('STRING');
        native.addTextComponentSubstringPlayerName(msg);
        native.setTextFont(fontType);
        native.setTextScale(1, scale);
        native.setTextWrap(0.0, 1.0);
        native.setTextCentre(true);
        native.setTextColour(rgba.r, rgba.g, rgba.b, rgba.a);

        if (useOutline) native.setTextOutline();

        if (useDropShadow) native.setTextDropShadow();

        native.endTextCommandDisplayText(0, 0, 0);
        native.clearDrawOrigin();
    }

    public static entityDrawText3d(entity: alt.Entity, msg: string, scale: number, fontType: number, rgba: alt.RGBA, useOutline = true, useDropShadow = true, isTME = false) {
        // let hex = msg.match('{.*}');
        // if (hex) {
        //     const rgb = hexToRgb(hex[0].replace('{', '').replace('}', ''));
        //     r = rgb[0];
        //     g = rgb[1];
        //     b = rgb[2];
        //     msg = msg.replace(hex[0], '');
        // }
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

        if (useOutline) native.setTextOutline();

        if (useDropShadow) native.setTextDropShadow();

        native.endTextCommandDisplayText(0, 0, 0);
        native.clearDrawOrigin();
    }
}