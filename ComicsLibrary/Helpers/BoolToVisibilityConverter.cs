using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ComicsLibrary.Helpers
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (bool) value;

            if (data)
            {
                return Visibility.Hidden;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (Visibility)value;

            return (data == Visibility.Hidden);
        }
    }
}
