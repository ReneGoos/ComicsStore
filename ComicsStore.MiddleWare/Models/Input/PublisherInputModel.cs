using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public class PublisherInputModel : BasicInputModel, IPublisherInputModel
    {
        public ICollection<BookPublisherInputModel> BookPublisher { get; set; }
    }
}