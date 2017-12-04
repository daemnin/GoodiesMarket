using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoodiesMarket.App.Components
{
    public static class DeviceHelper
    {
        static DeviceHelper() { }

        public static void OpenDeviceMap(double latitude, double longitude)
        {
            var location = $"{latitude},{longitude}";

            string requestUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    requestUri = $"http://maps.apple.com/maps?q=Ubicación&sll={location}";
                    break;
                case Device.Android:
                    requestUri = $"geo:0,0?q={location}(Ubicación)";
                    break;
                case Device.WinPhone:
                    requestUri = $"bingmaps:?cp={location}&q=Ubicación";
                    break;
            }

            Device.OpenUri(new Uri(requestUri));
        }

        public static async Task<Position> GetLocation()
        {
            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled) return null;

                locator.DesiredAccuracy = 100;

                position = await locator.GetPositionAsync(timeout: TimeSpan.FromSeconds(20), includeHeading: true);
            }
            catch (Exception)
            {

            }

            return position;
        }
    }
}
