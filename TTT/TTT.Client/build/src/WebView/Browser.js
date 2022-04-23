import * as alt from 'alt-client';
export default class Browser {
    static webView;
    static isCursorShown = false;
    static getInstance() {
        return new Promise(async (resolve) => {
            if (!Browser.webView) {
                Browser.webView = await this.createNewInstance();
            }
            Browser.webView.focus();
            resolve(Browser.webView);
        });
    }
    static showCursor(state) {
        if (this.isCursorShown == state) {
            return;
        }
        this.isCursorShown = state;
        alt.showCursor(state);
    }
    static showModule(module) {
        return new Promise(async (resolve, reject) => {
            this.webView.emit("TTT:View:ShowModule", module);
            await new Promise((resolve) => {
                this.webView.on("TTT:View:ModuleMounted", (moduleName) => {
                    if (moduleName === module)
                        resolve();
                });
            });
            resolve();
        });
    }
    static hideModule(module) {
        return new Promise(async (resolve, reject) => {
            this.webView.emit("TTT:View:HideModule", module);
            await new Promise((resolve) => {
                this.webView.on("TTT:View:ModuleUnmouted", (moduleName) => {
                    if (moduleName === module)
                        resolve();
                });
            });
            resolve();
        });
    }
    static createNewInstance() {
        return new Promise(async (resolve) => {
            let view = new alt.WebView("http://resource/client/view/index.html");
            await new Promise((resolve) => {
                view.on("TTT:View:Ready", () => { resolve(); });
            });
            resolve(view);
        });
    }
}
