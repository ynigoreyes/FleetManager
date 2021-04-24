using System;
namespace Simulation.DataContracts
{
    public class Notification
    {
        public string message { get; set; }
        public TruckStates TruckStates { get; set; }
        public DateTime dateTime { get; set; }
        public Guid id { get; private set; }

        public Notification(TruckStates _type, string _message)
        {
            id = Guid.NewGuid();
            TruckStates = _type;
            dateTime = DateTime.Now;
            message = _message;
        }
    }
}
