using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace ComicsLibrary.Helpers
{
    public class ArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (ICollection<string>) value;

            if (data is null)
            {
                return null;
            }

            var result = string.Join(',', data);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (string)value;

            if (data is null)
            {
                return null;
            }

            var result = new List<string>(data.Split(','));
            return result;
        }
    }
}
