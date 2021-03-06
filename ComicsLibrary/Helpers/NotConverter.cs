﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace ComicsLibrary.Helpers
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class NotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (bool)value;

            return !data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (bool)value;

            return !data;
        }
    }
}
