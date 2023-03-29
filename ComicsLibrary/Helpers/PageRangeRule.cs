using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace ComicsLibrary.Helpers
{
    public class PageRangeRule : ValidationRule
    {
        public Wrapper Wrapper { get; set; }

        public PageRangeRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int page = 0;

            try
            {
                if (((string)value).Length > 0)
                    page = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((page < Wrapper.MinPage) || (page > Wrapper.MaxPage))
            {
                return new ValidationResult(false,
                  $"Please enter an age in the range: {Wrapper.MinPage}-{Wrapper.MaxPage}.");
            }
            return ValidationResult.ValidResult;
        }
    }

    public class Wrapper : DependencyObject
    {
        public static readonly DependencyProperty MinPageProperty =
             DependencyProperty.Register("MinPage", typeof(int),
             typeof(Wrapper), new FrameworkPropertyMetadata(int.MaxValue));

        public static readonly DependencyProperty MaxPageProperty =
             DependencyProperty.Register("MaxPage", typeof(int),
             typeof(Wrapper), new FrameworkPropertyMetadata(int.MaxValue));

        public int MinPage
        {
            get { return (int)GetValue(MinPageProperty); }
            set { SetValue(MinPageProperty, value); }
        }
        public int MaxPage
        {
            get { return (int)GetValue(MaxPageProperty); }
            set { SetValue(MaxPageProperty, value); }
        }
    }
}
