using ComicsLibrary.ViewModels;
using ComicsStore.MiddleWare.Models.Output;
using System;
using System.Windows;
using System.Windows.Controls;

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
            (DataContext as ComicsViewModel).StoryView.NewCommand.Execute(null);
        }
    }
}
