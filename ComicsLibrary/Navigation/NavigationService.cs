using Microsoft.Extensions.DependencyInjection;
using SoftGoosR.Common.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ComicsLibrary.Navigation;

public class NavigationService(IServiceProvider serviceProvider) : ObservableObject, INavigationService
{
    private class NavigationContext(string windowKey
            , int? itemId
            , Action<int?, int?> HandleItem
            )
    {
        public string WindowKey { get; } = windowKey;
        public int? ItemId { get; } = itemId;
        public Action<int?, int?> HandleItem { get; } = HandleItem;
    }

    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private Frame _navigationFrame;
    private Window _navigationWindow;
    private string _title;

    private Dictionary<string, Type> Pages { get; } = [];
    private Dictionary<string, Page> LoadedPages { get; } = [];
    private Stack<NavigationContext> ActivePages { get; } = new Stack<NavigationContext>();

    private Dictionary<string, Type> Windows { get; } = [];

    public string PageChain => String.Join("<", ActivePages.Select(c => c.WindowKey));

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
        var window = await GetAndActivateWindowAsync(windowKey);
        window.Show();
    }

    public async Task<bool?> ShowPageAsync(string windowKey, int? itemId, Action<int?, int?> HandleItem)
    {
        ActivePages.Push(new NavigationContext(windowKey, itemId, HandleItem));
        RaisePropertyChanged("ActivePages");

        await SetPage(windowKey);

        return true;
    }

    public async Task HandleItem(int? itemId = null)
    {
        var currContext = ActivePages.Peek();
        currContext.HandleItem?.Invoke(itemId, currContext.ItemId);
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

    public bool LastPageActive(string windowKey)
    {
        if (ActivePages.Count <= 1)
        {
            return false;
        }
        var context = ActivePages.Take(2).Last();
        return context.WindowKey == windowKey && !ActivePages.First().ItemId.HasValue;
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
        if (!LoadedPages.TryGetValue(windowKey, out var value))
        {
            value = _serviceProvider.GetRequiredService(Pages[windowKey]) as Page;
            LoadedPages[windowKey] = value;
        }

        if (value.DataContext is IActivable activable)
        {
            await activable.ActivateAsync(parameter);
        }

        return value;
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
