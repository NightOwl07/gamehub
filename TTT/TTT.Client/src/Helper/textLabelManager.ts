import * as alt from 'alt-client';
// import Utils from '../Utils/utils';

export default class TextLabelManager {
    // private maxDistance: number = 50;
    private streamedInVehicles: alt.Vehicle[];

    constructor() {
        alt.on("gameEntityCreate", (entity) => {
            if (entity instanceof alt.Vehicle && this.streamedInVehicles) {
                this.streamedInVehicles[entity.id] = entity;
            }
        });

        alt.on("gameEntityDestroy", (entity) => {
            if (entity instanceof alt.Vehicle && this.streamedInVehicles) {
                delete this.streamedInVehicles[entity.id];
            }
        })
        alt.everyTick(this.onEveryTick);
    }

    public onEveryTick(): void {
        // var LocalPos = alt.Player.local.pos;

        // this.streamedInVehicles?.forEach(veh => {
        //     if (veh != null && veh.scriptID > 0) {
        //         let distance = Utils.distanceOf(veh.pos, LocalPos);
        //         if (distance <= this.maxDistance && alt.Player.local.vehicle == null) {
        //             let TME = veh.getStreamSyncedMeta('TME');
        //             if (TME && TME != "") {
        //                 Utils.entityDrawText3d(veh, `${TME}`, 0.27, 4, new alt.RGBA(194, 162, 218, 255));
        //             }
        //         }
        //     }
        // })
    }
}
