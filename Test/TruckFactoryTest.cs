using System;
using System.Collections.Generic;
using Simulation.DataContracts;
using Simulation.Objects;
using Xunit;

namespace Test
{
    public class TruckFactoryTest
    {
        [Fact]
        public void ShouldNotHaveHomeAsDestination()
        {
            TruckFactory f = new TruckFactory();

            for (int i = 0; i < 100; i++)
            {
                Truck newTruck = f.CreateTruck();
                Assert.False(newTruck.destination.Equals(Locations.Home));
            }
        }
    }
}
