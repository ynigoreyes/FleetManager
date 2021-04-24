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
        private static int numberOfTrucksCreated = 1;
        public INotificationQueue NotifQueue { get; private set; }

        public int id { get; private set; }
        public Tuple<double, double> destination { get; private set; }
        public DateTime createOn { get; private set; }
        public TruckStates State { get; private set; }
        public int Condition { get; private set; }
        private int waitTimer = 0;

        public double positionX { get; private set; }
        public double positionY { get; private set; }

        public Truck(INotificationQueue q)
        {
            NotifQueue = q;
            id = numberOfTrucksCreated;
            createOn = DateTime.Now;
            Condition = 100;

            positionX = -1;
            positionY = -1;
            numberOfTrucksCreated++;
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
                case TruckStates.LeavingOrigin:
                    State = TruckStates.AtDestination;
                    this.NotifQueue.AddPersistentNotificatiion(new Notification(TruckStates.AtDestination, $"Truck {this.id} has reached it's destination"));
                    break;
                case TruckStates.NeedRepair:
                    State = TruckStates.FinishedRepair;
                    break;
                case TruckStates.FinishedRepair:
                    destination = Tuple.Create(GlobalSettings.OriginCoord.Item1, GlobalSettings.OriginCoord.Item2);
                    Condition = 100;
                    State = TruckStates.LeavingDestination;
                    this.NotifQueue.AddPersistentNotificatiion(new Notification(TruckStates.FinishedRepair, $"Truck {this.id} has finished repairs"));
                    break;
                case TruckStates.AtOrigin:
                    Condition -= rnd.Next(5, 20);
                    this.SetRandomDestination();
                    State = TruckStates.LeavingOrigin;
                    this.NotifQueue.AddPersistentNotificatiion(new Notification(TruckStates.LeavingOrigin, $"Truck {this.id} is leaving origin."));
                    break;
                case TruckStates.AtDestination:
                    // Check if we need repairs before we go home
                    if (Condition < 90)
                    {
                        destination = Tuple.Create(GlobalSettings.RepairCoord.Item1, GlobalSettings.RepairCoord.Item2);
                        State = TruckStates.NeedRepair;
                        this.NotifQueue.AddPersistentNotificatiion(new Notification(TruckStates.NeedRepair, $"Truck {this.id} needs repairs"));
                    } else
                    {
                        destination = Tuple.Create(GlobalSettings.OriginCoord.Item1, GlobalSettings.OriginCoord.Item2);
                        State = TruckStates.LeavingDestination;
                        this.NotifQueue.AddPersistentNotificatiion(new Notification(TruckStates.LeavingDestination, $"Truck {this.id} is heading back to origin"));
                    }

                    waitTimer = rnd.Next(2, 6) * 100;
                    break;
                case TruckStates.LeavingDestination:
                    State = TruckStates.AtOrigin;
                    this.NotifQueue.AddPersistentNotificatiion(new Notification(TruckStates.AtOrigin, $"Truck {this.id} is has returned to origin"));
                    break;
                default:
                    break;
            }
        }

        public void SetStartingPoint(double startX, double startY)
        {
            this.State = TruckStates.LeavingOrigin;
            this.positionX = startX;
            this.positionY = startY;
            this.NotifQueue.AddPersistentNotificatiion(new Notification(TruckStates.LeavingOrigin, $"Truck {this.id} is leaving origin."));
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
