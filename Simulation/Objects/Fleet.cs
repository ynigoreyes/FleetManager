using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Simulation.Objects
{
    public class Fleet
    {
        public ITruckFactory truckFactory { get; private set; }
        public HashSet<Truck> fleet { get; private set; }

        public Fleet(int numberOfVehicles, ITruckFactory truckFactory)
        {
            fleet = new HashSet<Truck>();
            this.truckFactory = truckFactory;

            for (int i = 0; i < numberOfVehicles; i++)
            {
                fleet.Add(truckFactory.CreateTruck());
            }
        }

        public override string ToString()
        {
            foreach (var truck in fleet)
            {
                Console.WriteLine(truck);
            }
            return "";
        }
    }
}
