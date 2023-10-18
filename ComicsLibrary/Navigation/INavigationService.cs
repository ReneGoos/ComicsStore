using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;

namespace ComicsLibrary.Navigation
{
    public interface INavigationService : INotifyPropertyChanged
    {

        Window NavigationWindow { get; set; }
        Frame NavigationFrame { get; set; }
        string PageChain { get; }

        bool CanClose();
        Task ClosePageAsync(bool result, int? itemId = null);
        void Configure(string key, Type pageFile, bool isPage = true);
        bool PageActive(string windowKey);
        Task<bool?> ShowPageAsync(string windowKey, int? itemId, Action<int?, int?> AddItemToList);
        Task ShowWindowAsync(string windowKey);
    }
}