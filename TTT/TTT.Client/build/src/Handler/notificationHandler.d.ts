import * as alt from 'alt-client';
export default class NotificationHandler {
    private browserInstance;
    constructor(browserInstance: alt.WebView);
    sendNotification(type: number, title: string, message: string, duration?: number): void;
    private onNewNotification;
}
