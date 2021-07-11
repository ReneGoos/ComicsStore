using System;
using System.Globalization;
using System.Windows.Data;

namespace ComicsLibrary.Helpers
{
    [ValueConversion(typeof(object), typeof(bool))]
    public class IsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var test = value == null;

            return test;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
