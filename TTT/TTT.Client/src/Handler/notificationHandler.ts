import * as alt from 'alt-client';

export default class NotificationHandler {
    private browserInstance: alt.WebView;
    
    constructor(browserInstance: alt.WebView) {
        this.browserInstance = browserInstance;

        alt.onServer("TTT:NotificationHandler:NewNotification", this.onNewNotification.bind(this));
    }

    public sendNotification(type: number, title: string, message: string, duration: number = 3500): void {
        this.onNewNotification(type, title, message, duration);
    }

    private onNewNotification(type: number, title: string, message: string, duration: number): void {
        this.browserInstance?.emit("TTT:NotificationView:NewNotification", type, title, message, duration);
    }
}