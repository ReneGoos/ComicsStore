using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsLibrary.Helpers
{
    public interface ICollectionItem
    {
        bool ItemContains(string value);
        SortDescriptionCollection ItemSort(List<string> sortKey);
    }
}
