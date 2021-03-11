using System.Collections.Generic;

namespace TasteUfes.Services.Notifications
{
    public interface INotificator
    {
        void Handle(Notification notification);
        bool HasNotifications();
        bool HasSuccess();
        bool HasWarnings();
        bool HasErrors();
        List<Notification> GetNotifications();
        List<Notification> GetSuccess();
        List<Notification> GetWarnings();
        List<Notification> GetErrors();
    }
}
