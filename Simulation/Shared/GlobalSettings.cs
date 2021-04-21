using System;
namespace Simulation.Shared
{
    public class GlobalSettings
    {
        public static readonly int BALL_RADIUS = 20;
        public static readonly long HEIGHT = 1000;
        public static readonly long WIDTH = 1000;
        public static Tuple<double, double> OriginCoord = Tuple.Create<double, double>(BALL_RADIUS, (HEIGHT / 2) + (BALL_RADIUS + 10));
        public static Tuple<double, double> RepairCoord = Tuple.Create<double, double>((WIDTH / 2) + (BALL_RADIUS + 10), BALL_RADIUS);
        public static Tuple<double, double> Dest1Coord = Tuple.Create<double, double>(WIDTH + BALL_RADIUS, (HEIGHT / 2) + (BALL_RADIUS + 10));
        public static Tuple<double, double> Dest2Coord = Tuple.Create<double, double>((WIDTH / 2) + (BALL_RADIUS + 10), HEIGHT + BALL_RADIUS + 10);
        public static Tuple<double, double> FirstTruckStartingPoint = Tuple.Create<double, double>(OriginCoord.Item1 + 150, OriginCoord.Item2);
    }
}
