﻿using ComicsLibrary.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ComicsEntry.Views
{
    /// <summary>
    /// Interaction logic for OriginStoryWindow.xaml
    /// </summary>
    public partial class OriginStoryWindow : Window
    {
        public OriginStoryWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var basicView = (sender as Button).DataContext as BasicViewModel;

            basicView.SaveCommand.Execute(null);

            if (!basicView.IsDirty)
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