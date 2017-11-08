using GoodiesMarket.Components.Extensions;
using System;

namespace GoodiesMarket.Components.Helpers
{
    public class DistanceHelper
    {
        private static DistanceHelper instance;
        public static DistanceHelper Instance
        {
            get { return instance ?? (instance = new DistanceHelper()); }
        }

        private DistanceHelper() { }

        private const double EarthRadiusInMethers = 6378137;

        public double Calculate(double lat1, double lng1, double lat2, double lgn2)
        {
            var distance = 0d;

            var Lat = (lat2 - lat1).ToRadians();
            var Lon = (lgn2 - lat1).ToRadians();

            var arc = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(lat1.ToRadians()) * Math.Cos(lat2.ToRadians()) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            var curvature = 2 * Math.Atan2(Math.Sqrt(arc), Math.Sqrt(1 - arc));

            distance = EarthRadiusInMethers * curvature;

            return distance;
        }
    }
}
