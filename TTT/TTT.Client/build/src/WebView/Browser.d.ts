import * as alt from 'alt-client';
export default class Browser {
    private static webView;
    private static isCursorShown;
    static getInstance(): Promise<alt.WebView>;
    static showCursor(state: boolean): void;
    static showModule(module: string): Promise<void>;
    static hideModule(module: string): Promise<void>;
    private static createNewInstance;
}
