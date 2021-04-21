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
        public Tuple<double, double> destination { get; private set; }
        public DateTime createOn { get; private set; }

        public double positionX { get; private set; }
        public double positionY { get; private set; }

        public Truck(INotificationQueue q)
        {
            NotifQueue = q;
            id = Guid.NewGuid();
            createOn = DateTime.Now;

            positionX = -1;
            positionY = -1;
        }

        public void SetDestination(double destX, double destY)
        {
            destination = new(destX, destY);
        }

        public void Move()
        {
            if (Math.Abs(positionX - destination.Item1) > 0)
            {
                if (positionX > destination.Item1)
                {
                    positionX -= 1;
                } else
                {
                    positionX += 1;
                }
            } else if (Math.Abs(positionY - destination.Item2) > 0)
            {
                if (positionY > destination.Item2)
                {
                    positionY -= 1;
                }
                else
                {
                    positionY += 1;
                }
            }

            return;
        }

        public void SetStartingPoint(double startX, double startY)
        {
            this.positionX = startX;
            this.positionY = startY;
        }
    }
}
