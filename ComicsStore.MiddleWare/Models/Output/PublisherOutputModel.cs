using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Output
{
    public class PublisherOutputModel : BasicOutputModel
    {
        public ICollection<BasicBookOutputModel> BookPublisher { get; set; }
    }
}