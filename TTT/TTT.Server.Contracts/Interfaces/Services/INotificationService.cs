using AltV.Net.Elements.Entities;

namespace TTT.Server.Contracts.Interfaces.Services
{
    public interface INotificationService
    {
        public void SendErrorNotification(IPlayer player, string title, string message,
            int durationInMilliseconds = 3500);

        public void SendWarningNotification(IPlayer player, string title, string message,
            int durationInMilliseconds = 3500);

        public void SendInfoNotification(IPlayer player, string title, string message,
            int durationInMilliseconds = 3500);

        public void SendSuccessNotification(IPlayer player, string title, string message,
            int durationInMilliseconds = 3500);
    }
}