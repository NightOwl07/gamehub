import { WebView } from "alt-client";
import { WebviewInterface } from "./webview.interface";
import Browser from "../WebView/Browser";

declare module 'alt-client' {
    export interface WebView extends WebviewInterface { }
}

WebView.prototype.showModule = function(moduleName: string): Promise<void> {
    return Browser.showModule(moduleName);
};

WebView.prototype.hideModule = function(moduleName: string): Promise<void> {
    return Browser.hideModule(moduleName);
};