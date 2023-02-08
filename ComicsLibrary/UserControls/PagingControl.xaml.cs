using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace ComicsLibrary.UserControls
{
    /// <summary>
    /// Interaction logic for PagingControl.xaml
    /// </summary>
    public partial class PagingControl : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty PageProperty;
        public static readonly DependencyProperty TotalPagesProperty;
        public static readonly DependencyProperty PageSizeProperty;

        public ICollection ItemsSource
        {
            get
            {
                return GetValue(ItemsSourceProperty) as ICollection;
            }
            set
            {
                TotalPages = CalculateTotalPages((uint)value.Count, PageSize);
                SetValue(ItemsSourceProperty, value);
            }
        }

        public uint Page
        {
            get
            {
                return (uint)GetValue(PageProperty);
            }
            set
            {
                SetValue(PageProperty, value);
                FirstPageButton.IsEnabled = value > 1;
                PreviousPageButton.IsEnabled = value > 1;
                NextPageButton.IsEnabled = value < TotalPages;
                LastPageButton.IsEnabled = value < TotalPages;
            }
        }

        public uint TotalPages
        {
            get
            {
                return (uint)GetValue(TotalPagesProperty);
            }
            protected set
            {
                SetValue(TotalPagesProperty, value);
            }
        }

        public uint PageSize
        {
            get
            {
                return (uint)GetValue(PageSizeProperty);
            }
            protected set
            {
                if (ItemsSource is not null && PageSize > 0)
                {
                    TotalPages = CalculateTotalPages((uint)ItemsSource.Count, PageSize);
                }
                SetValue(ItemsSourceProperty, value);
            }
        }

        static PagingControl()
        {
            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(ICollection), typeof(PagingControl));
            PageProperty = DependencyProperty.Register("Page", typeof(uint), typeof(PagingControl), new PropertyMetadata((uint)1));
            TotalPagesProperty = DependencyProperty.Register("TotalPages", typeof(uint), typeof(PagingControl));
            PageSizeProperty = DependencyProperty.Register("PageSize", typeof(uint), typeof(PagingControl), new PropertyMetadata((uint)10));
        }

        public PagingControl()
        {
            InitializeComponent();
            Page = 1;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (!e.Property.Name.StartsWith("Is"))
            {
                switch (e.Property.Name)
                {
                    case "Page":
                        if (Page <= 0)
                        {
                            Page = 1;
                        }
                        else if (Page > TotalPages)
                        {
                            Page = TotalPages;
                        }
                        break;
                    case "ItemsSource":
                        if (PageSize > 0)
                        {
                            TotalPages = CalculateTotalPages((uint)((ICollection)e.NewValue).Count, PageSize);
                            Page = 1;
                        }
                        break;
                }
            }
            base.OnPropertyChanged(e);
        }

        private uint CalculateTotalPages(uint count, uint size)
        {
            if (size > 0)
            {
                return ((uint)(count - 1) / size) + 1;
            }
            return 1;
        }

        private void FirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            Page = 1;
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (Page > 1) 
            { 
                Page--;
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (Page < TotalPages)
            {
                Page++;
            }

        }

        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            Page = TotalPages;
        }
    }
}
