import NotificationHandler from "./Handler/notificationHandler";
declare class TTTClient {
    notificationService: NotificationHandler;
    private browserInstance;
    constructor();
    SetupAsync(): Promise<void>;
    private onEveryTick;
    private setupLoginScene;
}
export declare const Client: TTTClient;
export {};
