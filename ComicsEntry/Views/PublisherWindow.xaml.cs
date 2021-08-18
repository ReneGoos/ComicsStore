using ComicsLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ComicsEntry.Views
{
    /// <summary>
    /// Interaction logic for PublisherWindow.xaml
    /// </summary>
    public partial class PublisherWindow : Window
    {
        public PublisherWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var publisherView = (sender as Button).DataContext as PublisherViewModel;

            //if (publisherView.IsDirty)
            publisherView.SaveCommand.Execute(null);

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
