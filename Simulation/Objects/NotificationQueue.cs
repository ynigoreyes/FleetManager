using System;
using System.Collections.Generic;
using Simulation.DataContracts;

namespace Simulation.Objects
{
    public class NotificationQueue : INotificationQueue
    {
        private Notification _mostRecentNotif = null;

        public void AddPersistentNotificatiion(Notification notif)
        {
            Notification temp = _mostRecentNotif;
            _mostRecentNotif = notif;
            notif.SetNextNotif(temp);
        }

        public IEnumerable<Notification> ReadAllNotifications()
        {
            List<Notification> notifications = new List<Notification>();
            Notification tempNotif = _mostRecentNotif;
            do
            {
                notifications.Add(tempNotif);
                tempNotif = tempNotif.ReadNextNotification();
            } while (tempNotif != null);

            return notifications;
        }

        public void RemoveNotification(Guid notifId)
        {
            Notification tempCurrentNotif = _mostRecentNotif;
            Notification nextNotif = tempCurrentNotif.ReadNextNotification();

            while (nextNotif != null)
            {
                if (Equals(nextNotif.id, notifId))
                {
                    tempCurrentNotif.SetNextNotif(nextNotif.ReadNextNotification());
                    return;
                } else
                {
                    tempCurrentNotif = nextNotif;
                }
            }
        }
    }
}
