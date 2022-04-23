import * as alt from 'alt-client';
export default class AccountHandler {
    private browserInstance;
    constructor(browserInstance: alt.WebView);
    onShowWelcome(): Promise<void>;
    private onRegisterSuccess;
    private onLoginSuccess;
    private onAuthenticationFailed;
    private onViewLoginComplete;
    private onViewRegisterComplete;
    private cleanUp;
}
