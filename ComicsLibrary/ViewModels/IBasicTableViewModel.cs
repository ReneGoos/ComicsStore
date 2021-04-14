using ComicsLibrary.EditModels;
using System.ComponentModel;

namespace ComicsLibrary.ViewModels
{
    public interface IBasicTableViewModel<TEdit> where TEdit : TableEditModel, new()
    {
        TEdit Item { get; }
        string ItemFilter { get; set; }
        ICollectionView Items { get; }
    }
}