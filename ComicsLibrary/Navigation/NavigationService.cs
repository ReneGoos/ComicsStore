using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ComicsLibrary.Navigation
{
    public class NavigationService
    {
        private Dictionary<string, Type> Windows { get; } = new Dictionary<string, Type>();
        private List<string> ActiveDialogWindows { get; } = new List<string>();

        private readonly IServiceProvider _serviceProvider;

        public void Configure(string key, Type windowType) => Windows.Add(key, windowType);

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ShowAsync(string windowKey, object parameter = null)
        {
            var window = await GetAndActivateWindowAsync(windowKey, parameter);
            window.Show();
        }

        public async Task<bool?> ShowDialogAsync(string windowKey, object parameter = null)
        {
            var window = await GetAndActivateWindowAsync(windowKey, parameter);
            
            ActiveDialogWindows.Add(windowKey);
            var result = window.ShowDialog();
            ActiveDialogWindows.Remove(windowKey);

            return result;
        }

        private async Task<Window> GetAndActivateWindowAsync(string windowKey, object parameter = null)
        {
            var window = _serviceProvider.GetRequiredService(Windows[windowKey]) as Window;

            if (window.DataContext is IActivable activable)
            {
                await activable.ActivateAsync(parameter);
            }

            return window;
        }

        public bool DialogActive(string windowKey)
        {
            return ActiveDialogWindows.Contains(windowKey);
        }
    }
}
