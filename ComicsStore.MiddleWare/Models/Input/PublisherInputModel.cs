using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class PublisherInputModel : BasicInputModel
    {
        public ICollection<BookPublisherInputModel> BookPublisher { get; set; }
    }
}