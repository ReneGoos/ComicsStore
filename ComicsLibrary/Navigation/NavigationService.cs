using ComicsLibrary.Core;
using ComicsLibrary.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComicsLibrary.Navigation
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private class NavigationContext
        {
            public NavigationContext(string windowKey
                , int? itemId
                , Action<int?, int?> HandleItem
                )
            {
                WindowKey = windowKey;
                ItemId = itemId;
                this.HandleItem = HandleItem;
            }

            public string WindowKey { get; }
            public int? ItemId { get; }
            public Action<int?, int?> HandleItem { get; }
        }

        private readonly IServiceProvider _serviceProvider;
        private NavigateWindow _navigationWindow = null;

        private Dictionary<string, Type> Pages { get; } = new Dictionary<string, Type>();
        private Stack<NavigationContext> ActivePages { get; } = new Stack<NavigationContext>();

        private Dictionary<string, Type> Windows { get; } = new Dictionary<string, Type>();

        public string PageChain
        {
            get
            {
                return String.Join("<", ActivePages.Select(c => c.WindowKey));
            }
        }

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Configure(string key, Type pageFile, bool isPage = true)
        {
            if (isPage)
            {
                Pages.Add(key, pageFile);
            }
            else
            {
                Windows.Add(key, pageFile);
            }
        }

        public async Task ShowWindowAsync(string windowKey)
        {
            var window = await GetAndActivateWindowAsync(windowKey, null);
            window.Show();
        }

        public async Task<bool?> ShowPageAsync(string windowKey, int? itemId, Action<int?, int?> AddItemToList)
        {
            ActivePages.Push(new NavigationContext(windowKey, itemId, AddItemToList));
            RaisePropertyChanged("ActivePages");

            await SetPage(windowKey, itemId);

            return true;
        }

        public async Task ClosePageAsync(bool result, int? itemId = null)
        {
            var context = ActivePages.Pop();
            RaisePropertyChanged("ActivePages");

            if (result && context.HandleItem != null)
            {
                context.HandleItem(itemId, context.ItemId);
            }

            if (ActivePages.Count > 0)
            {
                context = ActivePages.Peek();
                await SetPage(context.WindowKey, null);
            }
            else
            {
                _navigationWindow.Close();
            }
        }

        public bool PageActive(string windowKey)
        {
            return ActivePages.Any(c => c.WindowKey == windowKey);
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

        private async Task<Page> GetAndActivatePageAsync(string windowKey, object parameter = null)
        {
            var page = _serviceProvider.GetRequiredService(Pages[windowKey]) as Page;

            if (page.DataContext is IActivable activable)
            {
                await activable.ActivateAsync(parameter);
            }

            return page;
        }

        private async Task SetPage(string windowKey, int? id)
        {
            var page = await GetAndActivatePageAsync(windowKey);

            if (_navigationWindow == null || _navigationWindow.IsClosed)
            {
                _navigationWindow = new NavigateWindow(this);
                _navigationWindow.Show();
            }
            _navigationWindow.Title = page.Title;
            _navigationWindow.Main.Content = page;
            //_navigationWindow.Main.DataContext = page.DataContext;
            //_navigationWindow.Show();
        }
    }
}
