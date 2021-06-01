using ComicsLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ComicsLibrary.Views
{
    /// <summary>
    /// Interaction logic for SeriesWindow.xaml
    /// </summary>
    public partial class SeriesWindow : Window
    {
        public SeriesWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var seriesView = (sender as Button).DataContext as SeriesViewModel;

            //if (seriesView.IsDirty)
                seriesView.SaveCommand.Execute(null);

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
