using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ComicsLibrary.Navigation
{
    public interface INavigationService : INotifyPropertyChanged
    {
        string PageChain { get; }
        Task ClosePageAsync(bool result, int? id = null);
        void Configure(string key, Type pageFile, bool isPage = true);
        bool PageActive(string windowKey);
        Task<bool?> ShowPageAsync(string windowKey, int? itemId, Action<int?, int?> AddItemToList);
        Task ShowWindowAsync(string windowKey);
    }
}