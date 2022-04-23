import * as alt from "alt-client";
import './extends/alt-webview.prototype';
import './extends/alt-player.prototype';
import { Client } from './Client';
Client.SetupAsync()
    .then(() => {
    alt.log("[Client] Gamemode: Setup finished!");
})
    .catch(() => {
    alt.log("[Client] Gamemode: Setup failed, please contact support!");
});
