using ComicsLibrary.EditModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComicsLibrary.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static void HandleItem<T>(this ObservableCollection<T> list, int? id, int? itemId, int? oldItemId) where T : CrossEditModel, new()
        {
            if (itemId.HasValue)
            {
                if (!list.Any(s => s.LinkedId == itemId.Value))
                {
                    if (oldItemId.HasValue && list.Any(s => s.LinkedId == oldItemId.Value))
                    {
                        var item = list.First(s => s.LinkedId == oldItemId.Value);
                        item.LinkedId = itemId;
                    }
                    else
                    {
                        list.Add(new T
                        {
                            MainId = id,
                            LinkedId = itemId
                        });
                    }
                }
            }
            else
            {
                if (oldItemId.HasValue && list.Any(s => s.LinkedId == oldItemId.Value))
                {
                    var item = list.First(s => s.LinkedId == oldItemId.Value);
                    list.Remove(item);
                }
            }
        }
    }
}
