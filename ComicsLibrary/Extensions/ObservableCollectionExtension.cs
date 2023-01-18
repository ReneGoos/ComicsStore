using ComicsLibrary.EditModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComicsLibrary.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static void HandleItem<T, TChild>(this ObservableCollection<T> list, int? id, int? oldItemId, TChild childItem) 
            where T : BasicEditModel, ICrossEditModel, new()
            where TChild: TableEditModel
        {
            if (childItem is null)
            {
                if (oldItemId.HasValue && list.Any(s => s.LinkedId == oldItemId.Value))
                {
                    var oldItem = list.FirstOrDefault(s => s.LinkedId == oldItemId.Value);
                    if (oldItem != null)
                    {
                        list.Remove(oldItem);
                    }
                }
            }
            else
            { 
                if (!oldItemId.HasValue || (oldItemId.HasValue && childItem.Id != oldItemId.Value))
                {
                    var fetchItem = list.FirstOrDefault(s => s.LinkedId == childItem.Id);
                    if (fetchItem != null)
                    {
                        return;
                    }
                }

                T item;
                if (oldItemId.HasValue)
                {
                    item = (list.First(a => a.LinkedId == oldItemId.Value));
                }
                else
                {
                    item = new T
                    {
                        MainId = id
                    };
                }

                item.LinkedId = childItem?.Id;
                item.ChildItem = (TChild)childItem;

                if (!oldItemId.HasValue)
                {
                    list.Add(item);
                }
            }
        }
        
    }
}
