using System;
using System.Globalization;
using Xamarin.Forms;

namespace RecipeMobileApp.Converters
{
    public class VegStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVeg = (bool)value;
            return isVeg ? "Showing: Vegetarian Recipes 🥦" : "Showing: Non-Vegetarian Recipes 🍗";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
