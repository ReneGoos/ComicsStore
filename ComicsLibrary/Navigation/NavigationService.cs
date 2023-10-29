using ComicsLibrary.Core;
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
        private Frame _navigationFrame;
        private Window _navigationWindow;
        private string _title;

        private Dictionary<string, Type> Pages { get; } = new Dictionary<string, Type>();
        private Dictionary<string, Page> LoadedPages { get; } = new Dictionary<string, Page>();
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

        public Window NavigationWindow
        {
            get => _navigationWindow; set { _navigationWindow = value; _title = _navigationWindow.Title; }
        }

        public Frame NavigationFrame
        {
            get => _navigationFrame; set => _navigationFrame = value;
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

            await SetPage(windowKey);

            return true;
        }

        public async Task ClosePageAsync(bool result, int? itemId = null)
        {
            var currContext = ActivePages.Pop();
            RaisePropertyChanged("ActivePages");

            if (ActivePages.Count > 0)
            {
                var newContext = ActivePages.Peek();
                await SetPage(newContext.WindowKey, currContext);

                if (result && currContext.HandleItem != null)
                {
                    currContext.HandleItem(itemId, currContext.ItemId);
                }
            }
            else
            {
                _navigationWindow.Title = _title;
                _navigationFrame.Content = null;
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
            if (!LoadedPages.ContainsKey(windowKey))
            {
                LoadedPages[windowKey] = _serviceProvider.GetRequiredService(Pages[windowKey]) as Page;
            }

            if (LoadedPages[windowKey].DataContext is IActivable activable)
            {
                await activable.ActivateAsync(parameter);
            }

            return LoadedPages[windowKey];
        }

        private async Task SetPage(string windowKey, NavigationContext context = null)
        {
            var page = await GetAndActivatePageAsync(windowKey, context?.WindowKey);
            _navigationWindow.Title = page.Title;
            _navigationFrame.Content = page;
        }

        public bool CanClose()
        {
            if (ActivePages.Count == 0)
            {
                return true;
            }
            return ((ObservableObject)_navigationFrame.DataContext).IsClean;
        }
    }
}
