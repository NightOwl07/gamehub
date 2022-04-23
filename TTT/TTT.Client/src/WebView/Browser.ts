import * as alt from 'alt-client';

export default class Browser {
    private static webView: alt.WebView;
    private static isCursorShown: boolean = false;

    public static getInstance(): Promise<alt.WebView> {
        return new Promise<alt.WebView>(async (resolve) => {
            if (!Browser.webView) {
                Browser.webView = await this.createNewInstance();
            }

            Browser.webView.focus();

            resolve(Browser.webView);
        });
    }

    public static showCursor(state: boolean): void {
        if (this.isCursorShown == state) {
            return;
        }

        this.isCursorShown = state;
        alt.showCursor(state);
    }

    public static showModule(module: string): Promise<void> {
        return new Promise<void>(async (resolve, reject) => {
            this.webView.emit("TTT:View:ShowModule", module);

            await new Promise<void>((resolve) => {
                this.webView.on("TTT:View:ModuleMounted", (moduleName) => {
                    if (moduleName === module) resolve();
                });
            });

            resolve();
        });
    }

    public static hideModule(module: string): Promise<void> {
        return new Promise<void>(async (resolve, reject) => {
            this.webView.emit("TTT:View:HideModule", module);

            await new Promise<void>((resolve) => {
                this.webView.on("TTT:View:ModuleUnmouted", (moduleName) => {
                    if (moduleName === module) resolve();
                });
            });

            resolve();
        });
    }

    private static createNewInstance(): Promise<alt.WebView> {
        return new Promise<alt.WebView>(async (resolve) => {
            let view: alt.WebView = new alt.WebView("http://resource/client/view/index.html");

            await new Promise<void>((resolve) => {
                view.on("TTT:View:Ready", () => { resolve(); });
            });

            resolve(view);
        });
    }
}