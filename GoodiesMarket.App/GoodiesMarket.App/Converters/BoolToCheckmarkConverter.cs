using System;
using System.Globalization;
using Xamarin.Forms;

namespace GoodiesMarket.App.Converters
{
    public class BoolToCheckmarkConverter : IValueConverter
    {
        private const string CHECKMARK_URL = "ic_checkmark.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isActive = (bool)value;
            return isActive ? CHECKMARK_URL : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url = (string)value;

            return !string.IsNullOrEmpty(url);
        }
    }
}
