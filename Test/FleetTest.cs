using System;
using System.Collections.Generic;
using Simulation.DataContracts;
using Simulation.Objects;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class FleetTest
    {
        private ITestOutputHelper output;

        public FleetTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ShouldProperlyPopulateFleet()
        {
            TruckFactory f = new TruckFactory();
            Fleet fleet = new Fleet(10, f);
            output.WriteLine(fleet.ToString());
            Assert.Equal(10, fleet.fleet.Count);
        }
    }
}
