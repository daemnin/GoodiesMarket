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

        public double Calculate(double lat1, double lng1, double lat2, double lng2)
        {
            var distLat = (lat2 - lat1).ToRadians();
            var distLng = (lng2 - lng1).ToRadians();

            var arc = Math.Pow(Math.Sin(distLat / 2), 2) +
                      Math.Cos(lat1.ToRadians()) * Math.Cos(lat2.ToRadians()) *
                      Math.Pow(Math.Sin(distLng / 2), 2);

            var curvature = 2 * Math.Atan2(Math.Sqrt(arc), Math.Sqrt(1 - arc));

            return EarthRadiusInMethers * curvature;
        }
    }
}
