using System.Collections.Generic;
using System.ComponentModel;

namespace ComicsLibrary.Helpers
{
    public interface ICollectionItem
    {
        bool ItemContains(string value);
        SortDescriptionCollection ItemSort(List<string> sortKey);
    }
}
