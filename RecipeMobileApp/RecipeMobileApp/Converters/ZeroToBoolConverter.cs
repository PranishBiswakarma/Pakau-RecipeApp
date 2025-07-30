
using System;
using System.Globalization;
using Xamarin.Forms;

namespace RecipeMobileApp.Converters
{
    public class ZeroToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is int count && count == 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
           throw new NotImplementedException();
    }
}
