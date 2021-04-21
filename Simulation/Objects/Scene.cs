using System;
using System.Collections.Generic;

namespace Simulation.Objects
{
    public class Scene
    {
        public double WIDTH { get; private set; }
        public double HEIGHT { get; private set; }
        public void Resize(double width, double height) =>
        (WIDTH, HEIGHT) = (width, height);

        public HashSet<Truck> fleet { get; private set; }

        public Scene(ITruckFactory factory)
        {
            Fleet fleetManager = new(4, factory);
            this.fleet = fleetManager.fleet;
        }
    }
}
