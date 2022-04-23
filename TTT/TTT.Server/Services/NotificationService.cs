using AltV.Net.Elements.Entities;
using TTT.Server.Contracts.Enums;
using TTT.Server.Contracts.Interfaces.Services;

namespace TTT.Server.Services
{
    public class NotificationService : INotificationService
    {
        public void SendErrorNotification(IPlayer player, string title, string message,
            int durationInMilliseconds = 3500)
        {
            this.SendNotification(player, NotificationType.Error, title, message, durationInMilliseconds);
        }

        public void SendWarningNotification(IPlayer player, string title, string message,
            int durationInMilliseconds = 3500)
        {
            this.SendNotification(player, NotificationType.Warning, title, message, durationInMilliseconds);
        }

        public void SendInfoNotification(IPlayer player, string title, string message,
            int durationInMilliseconds = 3500)
        {
            this.SendNotification(player, NotificationType.Info, title, message, durationInMilliseconds);
        }

        public void SendSuccessNotification(IPlayer player, string title, string message,
            int durationInMilliseconds = 3500)
        {
            this.SendNotification(player, NotificationType.Success, title, message, durationInMilliseconds);
        }

        private void SendNotification(IPlayer player, NotificationType type, string title, string message,
            int durationInMilliseconds)
        {
            if (string.IsNullOrEmpty(message)) message = "No error message set!";

            player.Emit("TTT:NotificationHandler:NewNotification", (int)type, title, message, durationInMilliseconds);
        }
    }
}