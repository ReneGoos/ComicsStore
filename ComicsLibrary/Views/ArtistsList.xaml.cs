using ComicsLibrary.ViewModels;
using ComicsStore.MiddleWare.Models.Output;
using System.Windows;
using System.Windows.Controls;

namespace ComicsLibrary.Views
{
    /// <summary>
    /// Interaction logic for ArtistsList.xaml
    /// </summary>
    public partial class ArtistsList : Window
    {
        public ArtistsList()
        {
            InitializeComponent();
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var id = (e.AddedItems[0] as ArtistOutputModel).Id;
            _ = await (comboBox.DataContext as ArtistViewModel).GetArtistAsync(id);
        }
    }
}
