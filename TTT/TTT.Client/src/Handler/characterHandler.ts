import * as alt from 'alt-client';
import * as native from 'natives';

export default class CharacterHandler {

    private player: alt.Player;

    constructor() {

        this.player = alt.Player.local;

        alt.onServer("TTT:PlayerHandler:SwitchInPlayer", this.onSwitchInPlayer.bind(this));
        alt.onServer("TTT:PlayerHandler:FadeOutScreen", this.onFadeOutScreen.bind(this));
        alt.onServer("TTT:PlayerHandler:FadeInScreen", this.onFadeInScreen.bind(this));
    }

    private onFadeOutScreen(milliseconds: number): void {
        native.doScreenFadeOut(milliseconds);
    }

    private onFadeInScreen(milliseconds: number): void {
        native.doScreenFadeIn(milliseconds);
    }

    private onSwitchInPlayer(state: boolean, vector: alt.Vector3): Promise<void> {
        return new Promise((resolve, reject) => {
            if (state) {
                native.setEntityCoords(this.player, vector.x, vector.y, vector.z, false, false, false, true);
                native.freezeEntityPosition(alt.Player.local.scriptID, false);
                native.allowPlayerSwitchDescent();
                native.switchInPlayer(this.player);
            } else {
                native.stopPlayerSwitch();
                native.switchOutPlayer(alt.Player.local.scriptID, 0, 1);
            }
        });
    }
}