using System.ComponentModel;

namespace ComicsLibrary.Core;

internal interface INotifyItemChanged
{
    event ItemChangedEventHandler? ItemChanged;
}