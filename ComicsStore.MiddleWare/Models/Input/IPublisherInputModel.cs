using System.Collections.Generic;

namespace ComicsStore.MiddleWare.Models.Input
{
    public interface IPublisherInputModel : IBasicInputModel
    {
        ICollection<BookPublisherInputModel> BookPublisher { get; set; }
    }
}