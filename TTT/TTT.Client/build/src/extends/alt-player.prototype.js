import { Player } from "alt-client";
import { Client } from "../Client";
Player.prototype.canInteract = (time) => {
    time.setMilliseconds(time.getMilliseconds() + 350);
    return time <= new Date();
};
Player.prototype.sendNotification = (type, title, message, duration = 3500) => {
    Client.notificationService.sendNotification(type, title, message, duration);
};
