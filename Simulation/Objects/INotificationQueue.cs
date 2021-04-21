using System;
using System.Collections.Generic;
using Simulation.DataContracts;

namespace Simulation.Objects
{
    public interface INotificationQueue
    {
        public IEnumerable<Notification> ReadAllNotifications();
        public void AddPersistentNotificatiion(Notification notif);
        public void RemoveNotification(Guid notifId);
    }
}
