using System;
using System.Collections.Generic;
using Simulation.Shared;

namespace Simulation.Objects
{
    public class Scene
    {
        public double WIDTH { get; private set; }
        public double HEIGHT { get; private set; }
        public void Resize(double width, double height) =>
        (WIDTH, HEIGHT) = (width, height);

        public HashSet<Truck> Fleet { get; private set; }


        public Scene(ITruckFactory factory)
        {
            Fleet fleetManager = new(8, factory);
            this.Fleet = fleetManager.fleet;

            int truckRow = 0;
            int truckNumber = 1;
            double verticalOffset = 15;
            double horizontalOffset = 30;
            Boolean sideSwitch = true;

            foreach (var truck in this.Fleet)
            {
                if (truckNumber >= 2 && truckNumber%2 == 0)
                {
                    truckRow += 1;
                }

                if (truckNumber == 1)
                {
                    truck.SetStartingPoint(GlobalSettings.FirstTruckStartingPoint.Item1 - (horizontalOffset * truckRow), GlobalSettings.FirstTruckStartingPoint.Item2);
                } else
                {
                    if (sideSwitch)
                    {
                        truck.SetStartingPoint(GlobalSettings.FirstTruckStartingPoint.Item1 - (horizontalOffset * truckRow), GlobalSettings.FirstTruckStartingPoint.Item2 + verticalOffset);
                    } else
                    {
                        truck.SetStartingPoint(GlobalSettings.FirstTruckStartingPoint.Item1 - (horizontalOffset * truckRow), GlobalSettings.FirstTruckStartingPoint.Item2 - verticalOffset);
                    }
                }

                truckNumber += 1;
                sideSwitch = !sideSwitch;

                truck.SetRandomDestination();
            }
        }

        public void MoveTrucks()
        {
            foreach (var truck in this.Fleet)
            {
                truck.Move();
            }
        }
    }
}
