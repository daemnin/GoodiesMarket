using System;

namespace GoodiesMarket.Components.Extensions
{
    public static class MathExtensions
    {
        public static double ToRadians(this double value)
        {
            return value * (Math.PI / 180);
        }
    }
}