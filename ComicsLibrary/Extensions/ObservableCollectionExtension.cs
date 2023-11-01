using ComicsLibrary.EditModels;
using ComicsLibrary.EditModels.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComicsLibrary.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static bool HandleItem<T, TChild>(this ObservableCollection<T> list, int? id, int? oldItemId, TChild childItem) 
            where T : BasicEditModel, ICrossEditModel, new()
            where TChild: TableEditModel
        {
            if (childItem is null)
            {
                // item deleted outside or unlinked, remove from list
                if (oldItemId.HasValue && list.Any(s => s.LinkedId == oldItemId.Value))
                {
                    var oldItem = list.FirstOrDefault(s => s.LinkedId == oldItemId.Value);
                    if (oldItem != null)
                    {
                        list.Remove(oldItem);
                        return true;
                    }
                }
            }
            else
            {
                if (oldItemId.HasValue && childItem.Id == oldItemId.Value  && !list.Any(s => s.LinkedId == oldItemId.Value))
                {
                    // item updated outside, but not linked
                    return false;
                }

                if (!oldItemId.HasValue || (oldItemId.HasValue && childItem.Id != oldItemId.Value))
                {
                    // item replace only if not linked otherwise
                    var fetchItem = list.FirstOrDefault(s => s.LinkedId == childItem.Id);
                    if (fetchItem != null)
                    {
                        return false;
                    }
                }

                T item = default;
                if (oldItemId.HasValue)
                {
                    // if oldItem is removed, return null
                    item = list.FirstOrDefault(a => a.LinkedId == oldItemId.Value);
                    if (item is null)
                    { 
                        oldItemId = null;
                    }
                }

                item ??= new T
                    {
                        MainId = id
                    };

                item.ChildItem = (TChild)childItem;
                item.LinkedId = childItem?.Id;

                if (!oldItemId.HasValue)
                {
                    list.Add(item);
                    return true;
                }

                return (oldItemId != item.LinkedId);
            }
            return false;
        }
        
    }
}
