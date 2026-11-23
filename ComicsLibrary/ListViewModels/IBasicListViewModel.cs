using ComicsStore.MiddleWare.Models.Output;
using System.ComponentModel;

namespace ComicsLibrary.ListViewModels;

public interface IBasicListViewModel<TOut> where TOut : BasicOutputModel, new()
{
    void UpdateItemsList(TOut item, bool removeOnly = false);
    IEnumerable<TOut> Items { get; }
    Func<object, string, bool> NameFilter { get; }
}