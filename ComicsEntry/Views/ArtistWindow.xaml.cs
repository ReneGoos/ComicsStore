using ComicsLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ComicsEntry.Views
{
    /// <summary>
    /// Interaction logic for ArtistWindow.xaml
    /// </summary>
    public partial class ArtistWindow : Window
    {
        public ArtistWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var artistView = (sender as Button).DataContext as ArtistViewModel;

            //if (artistView.IsDirty)
            artistView.SaveCommand.Execute(null);

            if (!artistView.IsDirty)
            {
                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
