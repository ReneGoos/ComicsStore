using System.Collections.Generic;
using System.Threading.Tasks;
using ComicsStore.MiddleWare.Models.Input;
using ComicsStore.MiddleWare.Models.Output;
using ComicsStore.MiddleWare.Models.Search;

namespace ComicsStore.MiddleWare.Services
{
    public interface IPublishersService : IComicsStoreService<PublisherInputModel, PublisherInputModel, PublisherOutputModel, BasicSearchModel>
    {
    }
}