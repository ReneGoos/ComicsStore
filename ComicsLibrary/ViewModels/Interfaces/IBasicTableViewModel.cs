using ComicsLibrary.EditModels;
using ComicsStore.MiddleWare.Models.Output;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ComicsLibrary.ViewModels.Interfaces
{
    public interface IBasicTableViewModel<TEdit, TOut> : IBasicViewModel
        where TEdit : TableEditModel, new()
        where TOut : BasicOutputModel
    {
        TEdit Item { get; }
        Func<object,string,bool> NameFilter { get; }
        ICollectionView FilteredItems { get; }
        IEnumerable<TOut> Items { get; }
    }
}