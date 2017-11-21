using System;
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
    }
}
