using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using Simulation.DataContracts;

namespace Simulation.Objects
{
    public partial class Truck
    {
        public INotificationQueue NotifQueue { get; private set; }

        public Guid id { get; private set; }
        public Locations destination { get; private set; }
        public DateTime createOn { get; private set; }

        private const string color = "red";
        private int positionX = -1;
        private int positionY = -1;

        public Truck(INotificationQueue q)
        {
            NotifQueue = q;
            id = Guid.NewGuid();
            createOn = DateTime.Now;
        }

        public void SetDestination(Locations location)
        {
            destination = location;
        }

        public override string ToString()
        {
            return $"Truck {id} created on {createOn} heading to {Enum.GetName(destination)}";
        }
    }
}
