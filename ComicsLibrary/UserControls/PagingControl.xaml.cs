using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ComicsLibrary.UserControls
{
    /// <summary>
    /// Interaction logic for PagingControl.xaml
    /// </summary>
    public partial class PagingControl : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty 
            = DependencyProperty.Register("ItemsSource", typeof(ICollection), typeof(PagingControl));
        public static readonly DependencyProperty PageProperty 
            = DependencyProperty.Register("Page", typeof(int), typeof(PagingControl), new PropertyMetadata(1));
        public static readonly DependencyProperty TotalPagesProperty 
            = DependencyProperty.Register("TotalPages", typeof(int), typeof(PagingControl));
        public static readonly DependencyProperty PageSizeProperty 
            = DependencyProperty.Register("PageSize", typeof(int), typeof(PagingControl), new PropertyMetadata(10));

        public ICollection ItemsSource
        {
            get
            {
                return GetValue(ItemsSourceProperty) as ICollection;
            }
            set
            {
               SetValue(ItemsSourceProperty, value);
            }
        }

        public int Page
        {
            get
            {
                return (int)GetValue(PageProperty);
            }
            set
            {
                if (TotalPages == 0)
                {
                    value = 1;
                }
                else if (value < 1)
                {
                    value = 1;
                }
                else if (value > TotalPages)
                {
                    value = TotalPages;
                }

                SetValue(PageProperty, value);
                EnableButtons(value, TotalPages);
            }
        }

        public int TotalPages
        {
            get
            {
                return (int)GetValue(TotalPagesProperty);
            }
            set
            {
                SetValue(TotalPagesProperty, value);
                EnableButtons(Page, value);
            }
        }

        public int PageSize
        {
            get
            {
                return (int)GetValue(PageSizeProperty);
            }
            set
            {
                SetValue(PageSizeProperty, value);
            }
        }

        public PagingControl()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
            if (!e.Property.Name.StartsWith("Is"))
            {
                switch (e.Property.Name)
                {
                    case "Page":
                        if (Page <= 0)
                        {
                            SetValue(PageProperty, 0);
                        }
                        else if (Page > TotalPages)
                        {
                            SetValue(PageProperty, TotalPages);
                        }
                        EnableButtons(Page, TotalPages);
                        break;
                    case "TotalPages":
                        if (Page > TotalPages)
                        {
                            SetValue(PageProperty, TotalPages);
                        }
                        EnableButtons(Page, TotalPages);
                        break;
                }
            }
            base.OnPropertyChanged(e);
        }

        private void EnableButtons(int page, int totalPages)
        {
            FirstPageButton.IsEnabled = page > 1;
            PreviousPageButton.IsEnabled = page > 1;
            NextPageButton.IsEnabled = page < totalPages;
            LastPageButton.IsEnabled = page < totalPages;
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
