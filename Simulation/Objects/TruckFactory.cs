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

            Locations randomLocation = Locations.Home;
            Array allLocations = Enum.GetValues(typeof(Locations));

            while (randomLocation.Equals(Locations.Home))
            {    
                Random random = new Random();
                randomLocation = (Locations)allLocations.GetValue(random.Next(allLocations.Length));
            }

            newTruck.SetDestination(randomLocation);

            return newTruck;
        }
    }
}
