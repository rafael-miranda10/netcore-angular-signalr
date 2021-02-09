using MaquininhaTheos.Domain.Interfaces.Notificacoes;
using System.Collections.Generic;
using System.Linq;

namespace MaquininhaTheos.Domain.Notificacoes
{
    public class NotificationHandler : INotificationHandler
    {
        private List<Notification> _notifications;
        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }
        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
