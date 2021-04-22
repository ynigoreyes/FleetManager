using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blazor.Extensions;
using Microsoft.AspNetCore.Components;
using Simulation.DataContracts;
using Simulation.Shared;

namespace Simulation.Objects
{
    public partial class Truck : IEquatable<Truck>, IComparable<Truck>
    {
        public INotificationQueue NotifQueue { get; private set; }

        public Guid id { get; private set; }
        public Tuple<double, double> destination { get; private set; }
        public DateTime createOn { get; private set; }
        public NotificationType State { get; private set; }
        public int Condition { get; private set; }
        private int waitTimer = 0;

        public double positionX { get; private set; }
        public double positionY { get; private set; }

        public Truck(INotificationQueue q)
        {
            NotifQueue = q;
            id = Guid.NewGuid();
            createOn = DateTime.Now;
            Condition = 100;

            positionX = -1;
            positionY = -1;
        }

        public void SetDestination(double destX, double destY)
        {
            destination = new(destX, destY);
        }

        public void SetRandomDestination()
        {
            List<Tuple<double, double>> availableDestinations = new List<Tuple<double, double>>() { GlobalSettings.Dest1Coord, GlobalSettings.Dest2Coord };
            var random = new Random();
            int index = random.Next(availableDestinations.Count);
            destination = Tuple.Create(availableDestinations[index].Item1, availableDestinations[index].Item2);
        }

        public void Move()
        {
            if (waitTimer != 0)
            {
                waitTimer -= 1;
                return;
            }

            if (Math.Abs(positionX - destination.Item1) > 0)
            {
                if (positionX > destination.Item1)
                {
                    positionX -= 1;
                } else
                {
                    positionX += 1;
                }
            }
            else if (Math.Abs(positionY - destination.Item2) > 0)
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
            // Has reached Destination
            else
            {
                this.determineNextDestination();
            }

            return;
        }

        private void determineNextDestination()
        {
            Random rnd = new Random();

            switch (State)
            {
                case NotificationType.LeavingOrigin:
                    State = NotificationType.AtDestination;
                    break;
                case NotificationType.NeedRepair:
                    State = NotificationType.FinishedRepair;
                    break;
                case NotificationType.FinishedRepair:
                    destination = Tuple.Create(GlobalSettings.OriginCoord.Item1, GlobalSettings.OriginCoord.Item2);
                    Condition = 100;
                    State = NotificationType.LeavingDestination;
                    break;
                case NotificationType.AtOrigin:
                    Condition -= rnd.Next(5, 20);
                    this.SetRandomDestination();
                    State = NotificationType.LeavingOrigin;
                    break;
                case NotificationType.AtDestination:
                    // Check if we need repairs before we go home
                    if (Condition < 90)
                    {
                        destination = Tuple.Create(GlobalSettings.RepairCoord.Item1, GlobalSettings.RepairCoord.Item2);
                        State = NotificationType.NeedRepair;
                    } else
                    {
                        destination = Tuple.Create(GlobalSettings.OriginCoord.Item1, GlobalSettings.OriginCoord.Item2);
                        State = NotificationType.LeavingDestination;
                    }

                    waitTimer = rnd.Next(1, 4) * 100;
                    break;
                case NotificationType.LeavingDestination:
                    State = NotificationType.AtOrigin;
                    break;
                default:
                    break;
            }
        }

        public void SetStartingPoint(double startX, double startY)
        {
            this.State = NotificationType.LeavingOrigin;
            this.positionX = startX;
            this.positionY = startY;
        }

        public int CompareTo(Truck other)
        {
            if(other == null)
                return 1;

            else
                return this.id.CompareTo(other.id);
        }

        public bool Equals(Truck other)
        {
            return this.id == other.id;
        }
    }
}
