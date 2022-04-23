import { Player } from "alt-client";
import { Client } from "../Client";
import { PlayerInterface } from "./player.interface";

declare module 'alt-client' {
    export interface Player extends PlayerInterface { }
}

Player.prototype.canInteract = (time: Date) => {
    time.setMilliseconds(time.getMilliseconds() + 350);

    return time <= new Date();
}

Player.prototype.sendNotification = (type: number, title: string, message: string, duration: number = 3500) => {
    Client.notificationService.sendNotification(type, title, message, duration);
}