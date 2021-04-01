using ComicsLibrary.ViewModels;
using System;
using System.Windows;

namespace ComicsLibrary.Views
{
    /// <summary>
    /// Interaction logic for StoryWindow.xaml
    /// </summary>
    public partial class StoryWindow : Window
    {
        public StoryWindow()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            (DataContext as ComicsViewModel).StoryView.IsDirty = false;
        }
    }
}
