export interface PlayerInterface {
    canInteract(time: Date): boolean;
    sendNotification(type: number, title: string, message: string, duration?: number): void;
  }