import * as alt from 'alt-client';
export default class TextLabelManager {
    streamedInVehicles;
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
        });
        alt.everyTick(this.onEveryTick);
    }
    onEveryTick() {
    }
}
