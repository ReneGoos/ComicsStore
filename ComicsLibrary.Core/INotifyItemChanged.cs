namespace ComicsLibrary.Core;

public interface INotifyItemChanged
{
    event ItemChangedEventHandler? ItemChanged;
}