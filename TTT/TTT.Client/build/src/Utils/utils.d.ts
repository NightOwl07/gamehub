/// <reference types="types-client" />
/// <reference types="@altv/types-shared" />
import * as alt from 'alt-client';
export default abstract class Utils {
    static setPedIntoVehicle(veh: alt.Vehicle): void;
    static setVehicleEngineMultiplier(multiplier: number): void;
    static toggleGameControls(state: boolean, showCursor?: boolean): void;
    static getClosestVehicle(player: alt.Player, range?: number): [alt.Vehicle | undefined, number | undefined];
    static distanceOf(vec1: alt.Vector3, vec2: alt.Vector3): number;
    static drawText3d(msg: string, position: alt.Vector3, scale: number, fontType: number, rgba: alt.RGBA, useOutline?: boolean, useDropShadow?: boolean, layer?: number): void;
    static entityDrawText3d(entity: alt.Entity, msg: string, scale: number, fontType: number, rgba: alt.RGBA, useOutline?: boolean, useDropShadow?: boolean, isTME?: boolean): void;
}
