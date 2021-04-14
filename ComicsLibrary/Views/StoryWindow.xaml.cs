using ComicsLibrary.ViewModels;
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

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var storyView = (sender as Button).DataContext as StoryViewModel;

            if (storyView.IsDirty)
                storyView.SaveCommand.Execute(null);

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
