using ComicsLibrary.EditModels;
using ComicsStore.Data.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ComicsLibrary.Helpers
{
    public class RoleTypesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (ICollection<RoleType>) value;

            if (data is null)
            {
                return null;
            }

            var result = string.Join(',', data.Where(x => x.Checked).Select(x => x.Name).ToList());
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (string)value;

            if (data is null)
            {
                return null;
            }

            var result = CheckedArrayMapper<ArtistType>.GetCheckedList(new List<string>(data.Split(',')));
            return result;
        }
    }
}
