using System.Collections.Generic;

namespace ComicsStore.Data.Model.Interfaces
{
    public interface IBookPublisher
    {
        ICollection<BookPublisher> BookPublisher { get; set; }
    }
}