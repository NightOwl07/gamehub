import { WebView } from "alt-client";
import Browser from "../WebView/Browser";
WebView.prototype.showModule = function (moduleName) {
    return Browser.showModule(moduleName);
};
WebView.prototype.hideModule = function (moduleName) {
    return Browser.hideModule(moduleName);
};
