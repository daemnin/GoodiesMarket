using System;
using System.Globalization;
using Xamarin.Forms;

namespace GoodiesMarket.App.Converters
{
    public class RangeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var range = (int?)value;

            if (range == null) return "No se a asignado el alcance.";

            return $"Alcance: {(((float)range.Value) / 1000).ToString("F", CultureInfo.InvariantCulture)} km.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
