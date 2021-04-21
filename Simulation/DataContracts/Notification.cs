using System;
namespace Simulation.DataContracts
{
    public class Notification
    {
        public string message { get; set; }
        public NotificationType notificationType { get; set; }
        public DateTime dateTime { get; set; }
        public Guid id { get; private set; }

        private Notification nextNotif = null;

        public Notification()
        {
            id = Guid.NewGuid();
        }

        public void SetNextNotif(Notification notif)
        {
            nextNotif = notif;
        }

        public Notification ReadNextNotification() => nextNotif;
    }
}
