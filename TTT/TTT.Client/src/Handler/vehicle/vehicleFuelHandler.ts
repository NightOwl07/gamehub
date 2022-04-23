import * as alt from 'alt-client';

export default class VehicleFuelHandler {
    constructor() {
        alt.onServer("TTT:VehicleFuelHandler:UpdateFuel", this.onUpdateFuel.bind(this));
    }

    private onUpdateFuel(vehicle: alt.Vehicle): void {
        
    }
}