using System.Collections.Generic;

namespace ComicsStore.Data.Model.Interfaces
{
    public interface IBookSeries
    {
        ICollection<BookSeries> BookSeries { get; set; }
    }
}