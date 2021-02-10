using StoreFront.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace StoreFront.Views
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

        private async void GetButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            await (button.DataContext as ArtistViewModel).GetArtistAsync(123);
        }
    }
}
