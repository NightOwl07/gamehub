import * as alt from 'alt-client';
import native from "natives";
import Utils from '../Utils/utils';
export default class AccountHandler {
    browserInstance;
    constructor(browserInstance) {
        this.browserInstance = browserInstance;
        alt.onServer("TTT:AccountHandler:ShowWelcome", this.onShowWelcome.bind(this));
        alt.onServer("TTT:AccountHandler:RegisterSuccess", this.onRegisterSuccess.bind(this));
        alt.onServer("TTT:AccountHandler:LoginSuccess", this.onLoginSuccess.bind(this));
        alt.onServer("TTT:AccountHandler:AuthenticationFailed", this.onAuthenticationFailed.bind(this));
        this.browserInstance?.on("TTT:LoginView:LoginComplete", this.onViewLoginComplete.bind(this));
        this.browserInstance?.on("TTT:RegisterView:RegisterComplete", this.onViewRegisterComplete.bind(this));
    }
    async onShowWelcome() {
        return this.browserInstance?.showModule("welcome").then(() => {
            Utils.toggleGameControls(false);
        });
    }
    async onRegisterSuccess() {
        return this.browserInstance?.hideModule("welcome").then(() => {
            this.cleanUp();
        });
    }
    async onLoginSuccess() {
        return this.browserInstance?.hideModule("welcome").then(() => {
            this.cleanUp();
        });
    }
    onAuthenticationFailed() {
        this.browserInstance?.emit("TTT:LoginView:StopLoading");
    }
    onViewLoginComplete(username, password) {
        alt.emitServer("TTT:AccountHandler:Login", username, password);
    }
    onViewRegisterComplete(email, username, password) {
        alt.emitServer("TTT:AccountHandler:Register", email, username, password);
    }
    cleanUp() {
        Utils.toggleGameControls(true, false);
        native.destroyAllCams(false);
        native.renderScriptCams(false, false, 0, true, false, 0);
    }
}
