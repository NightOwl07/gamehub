import * as alt from 'alt-client';
import * as native from 'natives';
export default class CharacterHandler {
    player;
    constructor() {
        this.player = alt.Player.local;
        alt.onServer("TTT:PlayerHandler:SwitchInPlayer", this.onSwitchInPlayer.bind(this));
        alt.onServer("TTT:PlayerHandler:FadeOutScreen", this.onFadeOutScreen.bind(this));
        alt.onServer("TTT:PlayerHandler:FadeInScreen", this.onFadeInScreen.bind(this));
    }
    onFadeOutScreen(milliseconds) {
        native.doScreenFadeOut(milliseconds);
    }
    onFadeInScreen(milliseconds) {
        native.doScreenFadeIn(milliseconds);
    }
    onSwitchInPlayer(state, vector) {
        return new Promise((resolve, reject) => {
            if (state) {
                native.setEntityCoords(this.player, vector.x, vector.y, vector.z, false, false, false, true);
                native.freezeEntityPosition(alt.Player.local.scriptID, false);
                native.allowPlayerSwitchDescent();
                native.switchInPlayer(this.player);
            }
            else {
                native.stopPlayerSwitch();
                native.switchOutPlayer(alt.Player.local.scriptID, 0, 1);
            }
        });
    }
}
