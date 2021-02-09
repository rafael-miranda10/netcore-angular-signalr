using MaquininhaTheos.Domain.Notificacoes;
using System.Collections.Generic;

namespace MaquininhaTheos.Domain.Interfaces.Notificacoes
{
    public interface INotificationHandler
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
