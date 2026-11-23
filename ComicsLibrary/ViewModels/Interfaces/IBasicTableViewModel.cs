using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Output;
using System.ComponentModel;

namespace ComicsLibrary.ViewModels.Interfaces;

public interface IBasicTableViewModel<TEdit, TOut> : IBasicViewModel
    where TEdit : TableEditModel, new()
    where TOut : BasicOutputModel
{
    TEdit Item { get; }
    ICollectionView FilteredItems { get; }
}