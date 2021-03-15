using System.Collections.Generic;
using System.Linq;

namespace TasteUfes.Services.Notifications
{
    public class Notificator : INotificator
    {
        private readonly List<Notification> _notifications;

        public Notificator()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notificacao)
        {
            _notifications.Add(notificacao);
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }

        public List<Notification> GetSuccess()
        {
            return _notifications.Where(n => n.Type == NotificationType.SUCCESS).ToList();
        }

        public bool HasSuccess()
        {
            return _notifications.Any(n => n.Type == NotificationType.SUCCESS);
        }

        public List<Notification> GetErrors()
        {
            return _notifications.Where(n => n.Type == NotificationType.ERROR).ToList();
        }

        public bool HasErrors()
        {
            return _notifications.Any(n => n.Type == NotificationType.ERROR);
        }

        public List<Notification> GetWarnings()
        {
            return _notifications.Where(n => n.Type == NotificationType.WARNING).ToList();
        }

        public bool HasWarnings()
        {
            return _notifications.Any(n => n.Type == NotificationType.WARNING);
        }
    }
}
