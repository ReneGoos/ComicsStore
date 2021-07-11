using ComicsLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ComicsLibrary.Views
{
    /// <summary>
    /// Interaction logic for CodeWindow.xaml
    /// </summary>
    public partial class CodeWindow : Window
    {
        public CodeWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var codeView = (sender as Button).DataContext as CodeViewModel;

            //if (codeView.IsDirty)
            codeView.SaveCommand.Execute(null);

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
