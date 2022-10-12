using ComicsLibrary.Navigation;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace ComicsLibrary.Windows
{
    /// <summary>
    /// Interaction logic for NavigateWindow.xaml
    /// </summary>
    public partial class NavigateWindow : Window
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;
        const uint MF_ENABLED = 0x00000000;

        const uint SC_CLOSE = 0xF060;

        const int WM_SHOWWINDOW = 0x00000018;
        const int WM_CLOSE = 0x10;

        private readonly NavigationService _navigationService;

        public NavigateWindow(NavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            await _navigationService.ClosePageAsync(true);
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            await _navigationService.ClosePageAsync(false);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(this.hwndSourceHook));
            }
        }

        IntPtr hwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SHOWWINDOW)
            {
                IntPtr hMenu = GetSystemMenu(hwnd, false);
                if (hMenu != IntPtr.Zero)
                {
                    EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                }
            }
            //else if (msg == WM_CLOSE)
            //{
            //    handled = true;
            //}
            return IntPtr.Zero;
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.IsClosed = true;
        }

        public bool IsClosed { get; private set; }
    }
}
