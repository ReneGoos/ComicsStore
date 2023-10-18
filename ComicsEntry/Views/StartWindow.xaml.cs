using ComicsLibrary.Navigation;
using System.ComponentModel;
using System.Windows;

namespace ComicsEntry.Views
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private readonly INavigationService _navigationService;

        public StartWindow(INavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;

            _navigationService.NavigationWindow = this;
            _navigationService.NavigationFrame = Main;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = !_navigationService.CanClose();
        }
    }
}
