import * as alt from 'alt-client';
import native from "natives";
import Utils from '../Utils/utils';

export default class AccountHandler {
    private browserInstance: alt.WebView;

    constructor(browserInstance: alt.WebView) {
        this.browserInstance = browserInstance;

        alt.onServer("TTT:AccountHandler:ShowWelcome", this.onShowWelcome.bind(this));
        alt.onServer("TTT:AccountHandler:RegisterSuccess", this.onRegisterSuccess.bind(this));
        alt.onServer("TTT:AccountHandler:LoginSuccess", this.onLoginSuccess.bind(this));
        alt.onServer("TTT:AccountHandler:AuthenticationFailed", this.onAuthenticationFailed.bind(this));

        this.browserInstance?.on("TTT:LoginView:LoginComplete", this.onViewLoginComplete.bind(this));
        this.browserInstance?.on("TTT:RegisterView:RegisterComplete", this.onViewRegisterComplete.bind(this));
    }

    public async onShowWelcome(): Promise<void> {
        return this.browserInstance?.showModule("welcome").then(() => {
            Utils.toggleGameControls(false);
        });
    }

    private async onRegisterSuccess(): Promise<void> {
        return this.browserInstance?.hideModule("welcome").then(() => {
            this.cleanUp();
        });
    }

    private async onLoginSuccess(): Promise<void> {
        return this.browserInstance?.hideModule("welcome").then(() => {
            this.cleanUp();
        });
    }

    private onAuthenticationFailed(error: string): void {
        this.browserInstance?.emit("TTT:LoginView:StopLoading");
        this.browserInstance?.emit("TTT:RegisterView:AuthenticationFailed", error);
    }

    private onViewLoginComplete(username: string, password: string): void {
        alt.emitServer("TTT:AccountHandler:Login", username, password);
    }

    private onViewRegisterComplete(email: string, username: string, password: string): void {
        alt.emitServer("TTT:AccountHandler:Register", email, username, password);
    }

    private cleanUp(): void {
        Utils.toggleGameControls(true, false);
        native.destroyAllCams(false);
        native.renderScriptCams(false, false, 0, true, false, 0);
    }
}