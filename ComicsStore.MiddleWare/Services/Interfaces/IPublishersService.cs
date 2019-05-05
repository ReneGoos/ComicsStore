using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Services.Interfaces
{
    public interface IPublishersService : IComicsStoreService<PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearchModel>
    {
    }
}