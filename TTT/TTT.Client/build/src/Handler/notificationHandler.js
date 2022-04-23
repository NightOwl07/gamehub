import * as alt from 'alt-client';
export default class NotificationHandler {
    browserInstance;
    constructor(browserInstance) {
        this.browserInstance = browserInstance;
        alt.onServer("TTT:NotificationHandler:NewNotification", this.onNewNotification.bind(this));
    }
    sendNotification(type, title, message, duration = 3500) {
        this.onNewNotification(type, title, message, duration);
    }
    onNewNotification(type, title, message, duration) {
        this.browserInstance?.emit("TTT:NotificationView:NewNotification", type, title, message, duration);
    }
}
