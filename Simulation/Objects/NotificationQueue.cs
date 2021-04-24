using System;
using System.Collections.Generic;
using Simulation.DataContracts;

namespace Simulation.Objects
{
    public class NotificationQueue : INotificationQueue
    {
        private Queue<Notification> queue = new Queue<Notification>();
        private readonly int BUFFER_SIZE = 10;

        public void AddPersistentNotificatiion(Notification notif)
        {
            queue.Enqueue(notif);

            if (queue.Count > BUFFER_SIZE)
            {
                queue.Dequeue();
            }
        }

        public IEnumerable<Notification> ReadAllNotifications()
        {
            return queue.ToArray();
        }
    }
}
