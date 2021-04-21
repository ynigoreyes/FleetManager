using System;
using Simulation.DataContracts;

namespace Simulation.Objects
{
    public class TruckFactory : ITruckFactory
    {
        private INotificationQueue notifQueue { get; set; }

        public TruckFactory(INotificationQueue q)
        {
            notifQueue = q;
        }

        public Truck CreateTruck()
        {
            Truck newTruck = new Truck(notifQueue);

            return newTruck;
        }
    }
}
