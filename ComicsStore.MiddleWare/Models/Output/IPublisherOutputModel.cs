using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public interface IPublisherOutputModel
    {
        ICollection<PublisherBookOutputModel> BookPublisher { get; set; }
    }
}